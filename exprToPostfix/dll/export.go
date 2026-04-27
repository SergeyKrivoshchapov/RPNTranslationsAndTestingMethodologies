package main

/*
#include <stdlib.h>
*/
import "C"
var converter = Converter.NewConverter()

//export ToPoliz
//using this lib you need to check "ERROR: " prefix
func ToPoliz(expr *C.char) *C.char {
	go Expr := C.GoString(expr)

	polizStr, err := converter.ToPolizString(goExpr)
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
