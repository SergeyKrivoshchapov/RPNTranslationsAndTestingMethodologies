package parser

import (
	"fmt"
	"shared/stack"
	"strings"

	"exprToPostfix/lexer"
)

type Parser struct {
	priority map[string]int
}

func NewParser() *Parser {
	return &Parser{
		priority: map[string]int{
			"(": 0, ")": 1,
			"+": 2, "-": 2,
			"*": 3, "/": 3,
			"^": 4,
		},
	}
}

func (p *Parser) getPriority(op string, isUnary bool) int {
	if isUnary {
		return 5
	}

	if pri, ok := p.priority[op]; ok {
		return pri
	}
	return -1
}

func (p *Parser) ToPoliz(tokens []lexer.Token) ([]string, error) {
	var output []string
	opStack := stack.New[string]()

	for _, token := range tokens {
		switch token.Type {
		case lexer.TokenNumber:
			output = append(output, token.Value)

		case lexer.TokenLeftParen:
			opStack.Push("(")
		case lexer.TokenRightParen:
			for {
				top, ok := opStack.Pop()
				if !ok {
					return nil, fmt.Errorf("несогласованные скобки")
				}
				if top == "(" {
					break
				}
				output = append(output, top)
			}

		case lexer.TokenUnaryMinus:
			output = append(output, "u-")

		case lexer.TokenOperator:
			op := token.Value
			priority := p.getPriority(op, false)

			for !opStack.IsEmpty() {
				top, _ := opStack.Peek()
				if top == "(" {
					break
				}
				topPriority := p.getPriority(top, false)
				if topPriority >= priority {
					opStack.Pop()
					output = append(output, top)
				} else {
					break
				}
			}
			opStack.Push(op)
		}
	}

	for !opStack.IsEmpty() {
		top, _ := opStack.Pop()
		if top == "(" {
			return nil, fmt.Errorf("несогласованные скобки")
		}
		output = append(output, top)
	}

	return output, nil
}

func (p *Parser) ToPolizString(tokens []lexer.Token) (string, error) {
	poliz, err := p.ToPoliz(tokens)
	if err != nil {
		return "", err
	}
	return strings.Join(poliz, " "), nil
}
