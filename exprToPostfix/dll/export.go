package main

/*
#include <stdlib.h>
*/
import "C"

import (
	"unsafe"

	"exprToPostfix/converter"
)

var conv = converter.NewConverter()

// using this lib you need to check "ERROR: " prefix
//
//export ToPoliz
func ToPoliz(expr *C.char) *C.char {
	goExpr := C.GoString(expr)

	polizStr, err := conv.ToPoliz(goExpr)
	if err != nil {
		errMsg := "ERROR: " + err.Error()
		return C.CString(errMsg)
	}
	return C.CString(polizStr)
}

//export FreeString
func FreeString(str *C.char) {
	if str != nil {
		C.free(unsafe.Pointer(str))
	}
}

func main() {}
