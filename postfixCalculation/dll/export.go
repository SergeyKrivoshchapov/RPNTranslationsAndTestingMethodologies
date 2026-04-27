package main

/*
#include <stdlib.h>
*/
import "C"

import (
	"fmt"
	"unsafe"

	"postfixCalculation/calculator"
)

var calc = calculator.NewPostfixCalculator()

// Calculates postfix expressions
//
//export EvaluatePostfix
func EvaluatePostfix(expr *C.char) *C.char {
	goExpr := C.GoString(expr)
	result, err := calc.Calculate(goExpr)
	if err != nil {
		return C.CString("ERROR: " + err.Error())
	}

	return C.CString(fmt.Sprintf("%d", result))
}

func FreeString(str *C.char) {
	if str != nil {
		C.free(unsafe.Pointer(str))
	}
}

func main() {}
