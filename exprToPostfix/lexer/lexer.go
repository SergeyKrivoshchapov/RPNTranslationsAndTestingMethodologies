package lexer

import (
	"fmt"
	"unicode"
)

type TokenType int

type Token struct {
	Type  TokenType
	Value string
}

const (
	TokenNumber TokenType = iota
	TokenOperator
	TokenLeftParen
	TokenRightParen
	TokenUnknown
)

type Lexer struct {
	input string
	pos   int
}

func NewLexer(input string) *Lexer {
	return &Lexer{input: input, pos: 0}
}

func (l *Lexer) Tokenize() ([]Token, error) {
	var tokens []Token
	runes := []rune(l.input)

	for i := 0; i < len(runes); i++ {
		ch := runes[i]

		if unicode.IsSpace(ch) {
			continue
		}

		if unicode.IsDigit(ch) {
			start := i
			for i < len(runes) && unicode.IsDigit(runes[i]) {
				i++
			}
			num := string(runes[start:i])
			tokens = append(tokens, Token{Type: TokenNumber, Value: num})
			i--
			continue
		}

		token, err := l.makeToken(ch, tokens, i)
		if err != nil {
			return nil, err
		}
		tokens = append(tokens, token)
	}
	return tokens, nil
}

func (l *Lexer) isUnaryOp(tokens []Token, currentPos int) bool {
	if len(tokens) == 0 {
		return true
	}
	lastToken := tokens[len(tokens)-1]
	return lastToken.Type == TokenOperator || lastToken.Type == TokenLeftParen
}

func (l *Lexer) makeToken(ch rune, tokens []Token, pos int) (Token, error) {
	switch ch {
	case '(':
		return Token{Type: TokenLeftParen, Value: "("}, nil
	case ')':
		return Token{Type: TokenRightParen, Value: ")"}, nil
	case '+', '-', '*', '/', '^':
		if ch == '-' && l.isUnaryOp(tokens, pos) {
			return Token{Type: TokenOperator, Value: "u-"}, nil
		}
		return Token{Type: TokenOperator, Value: string(ch)}, nil
	default:
		return Token{Type: TokenUnknown, Value: string(ch)}, fmt.Errorf("Не известный символ %c", ch)
	}
}
