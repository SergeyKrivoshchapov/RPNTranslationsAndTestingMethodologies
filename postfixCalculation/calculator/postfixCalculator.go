package calculator

import (
	"errors"
	"fmt"
	"shared/stack"
	"strconv"
	"strings"
)

type PostfixCalculator struct{}

func NewPostfixCalculator() *PostfixCalculator {
	return &PostfixCalculator{}
}

func (c *PostfixCalculator) Calculate(postfixExpr string) (int, error) {
	if strings.TrimSpace(postfixExpr) == "" {
		return 0, errors.New("пустое выражение")
	}

	tokens := strings.Fields(postfixExpr)

	if len(tokens) == 0 {
		return 0, errors.New("пустое выражение")
	}

	valStack := stack.New[int]()

	for _, token := range tokens {
		if num, err := strconv.Atoi(token); err == nil {
			valStack.Push(num)
			continue
		}

		if token == "u-" {
			val, ok := valStack.Pop()
			if !ok {
				return 0, fmt.Errorf("Недостаточно операндов для унарного минуса")
			}
			valStack.Push(-val)
			continue
		}

		if valStack.Size() < 2 {
			return 0, fmt.Errorf("Недостаточно операндов для оператора %s", token)
		}

		b, _ := valStack.Pop()
		a, _ := valStack.Pop()

		result, err := c.applyOperator(token, a, b)
		if err != nil {
			return 0, err
		}
		valStack.Push(result)
	}

	result, _ := valStack.Pop()
	return result, nil
}

func (c *PostfixCalculator) applyOperator(op string, a, b int) (int, error) {
	switch op {
	case "+":
		return a + b, nil
	case "-":
		return a - b, nil
	case "*":
		return a * b, nil
	case "/":
		if b == 0 {
			return 0, fmt.Errorf("Деление на ноль")
		}
		return a / b, nil
	case "^":
		if b < 0 {
			return 0, fmt.Errorf("Отрицательная степень не поддерживается")
		}
		result := 1
		for i := 0; i < b; i++ {
			result *= a
		}
		return result, nil
	default:
		return 0, fmt.Errorf("Неизвестный оператор: %s", op)
	}
}
