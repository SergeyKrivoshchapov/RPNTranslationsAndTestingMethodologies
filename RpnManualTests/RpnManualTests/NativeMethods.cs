using System;
using System.Runtime.InteropServices;

namespace RpnManualTests;

public static class NativeMethods
{
    private const string InfixDll = "exprToPostfix.dll";
    private const string PostfixDll = "postfixCalculation.dll";

    [DllImport(InfixDll, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr ToPoliz(string expr);

    [DllImport(InfixDll, CallingConvention = CallingConvention.Cdecl)]
    private static extern void FreeString(IntPtr str);

    [DllImport(PostfixDll, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr EvaluatePostfix(string expr);

    public static string ConvertToPoliz(string expr)
    {
        IntPtr p = ToPoliz(expr ?? string.Empty);
        if (p == IntPtr.Zero) return string.Empty;
        try { return Marshal.PtrToStringUTF8(p) ?? string.Empty; }
        finally { FreeString(p); } // есть экспорт в exprToPostfix
    }

    public static string CalculatePostfix(string expr)
    {
        IntPtr p = EvaluatePostfix(expr ?? string.Empty);
        if (p == IntPtr.Zero) return string.Empty;

        // В postfix DLL FreeString не экспортирован в вашем коде.
        // Поэтому только чтение строки.
        return Marshal.PtrToStringUTF8(p) ?? string.Empty;
    }
}