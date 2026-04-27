package converter

import (
	"exprToPostfix/lexer"
	"exprToPostfix/parser"
)

type Converter struct {
	parser *parser.Parser
}

func NewConverter() *Converter {
	return &Converter{
		parser: parser.NewParser(),
	}
}

func (c *Converter) ToPoliz(expression string) (string, error) {
	l := lexer.NewLexer(expression)
	tokens, err := l.Tokenize()
	if err != nil {
		return "", err
	}

	return c.parser.ToPolizString(tokens)
}

func (c *Converter) Validate(expression string) bool {
	_, err := c.ToPoliz(expression)
	return err == nil
}
