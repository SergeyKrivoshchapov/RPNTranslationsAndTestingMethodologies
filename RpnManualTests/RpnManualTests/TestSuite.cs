using System;
using System.Collections.Generic;
using System.Linq;

namespace RpnManualTests;

public static class TestSuite
{
    private sealed class TableWidths
    {
        public int Input { get; init; }
        public int Path { get; init; }
        public int Expected { get; init; }
        public int Actual { get; init; }
        public int Verdict { get; init; }
    }

    public static List<TestCase> BuildAll() => new()
    {
        // ===== ЗАДАНИЕ 1: ToPoliz =====
        new() { Id="T1-EC1", Task="Task1", Method="EC", Input="2+3", Path="num->op", Expected="2 3 +" },
        new() { Id="T1-EC2", Task="Task1", Method="EC", Input="(2+3)*4", Path="( ) + *", Expected="2 3 + 4 *" },
        new() { Id="T1-EC3", Task="Task1", Method="EC", Input="-2+3", Path="унарный минус", Expected="u- 2 3 +" },
        new() { Id="T1-EC4", Task="Task1", Method="EC", Input="2+a", Path="неизвестный символ", Expected="ERROR: Не известный символ a" },

        new() { Id="T1-BV1", Task="Task1", Method="BV", Input="", Path="пустой ввод", Expected="" },
        new() { Id="T1-BV2", Task="Task1", Method="BV", Input="   ", Path="пробелы", Expected="" },
        new() { Id="T1-BV3", Task="Task1", Method="BV", Input=")", Path="скобка без пары", Expected="ERROR: несогласованные скобки" },

        new() { Id="T1-CE1", Task="Task1", Method="CE", Input="1+2*3", Path="приоритеты", Expected="1 2 3 * +" },
        new() { Id="T1-CE2", Task="Task1", Method="CE", Input="(1+2", Path="скобки не согласованы", Expected="ERROR: несогласованные скобки" },

        new() { Id="T1-EG1", Task="Task1", Method="EG", Input="--2", Path="цепочка унарных", Expected="u- u- 2" },
        new() { Id="T1-EG2", Task="Task1", Method="EG", Input="2+3)", Path="лишняя правая скобка", Expected="ERROR: несогласованные скобки" },

        // White-box (1..5)
        new() { Id="T1-WB1", Task="Task1", Method="WB-S", Input="1+2", Path="покрытие операторов", Expected="1 2 +" },
        new() { Id="T1-WB2", Task="Task1", Method="WB-B", Input="(1+2)*3", Path="ветви if/switch true/false", Expected="1 2 + 3 *" },
        new() { Id="T1-WB3", Task="Task1", Method="WB-C", Input="2+a", Path="условие unknown=true", Expected="ERROR: Не известный символ a" },
        new() { Id="T1-WB4", Task="Task1", Method="WB-DC", Input="(1+2", Path="решения+условия", Expected="ERROR: несогласованные скобки" },
        new() { Id="T1-WB5", Task="Task1", Method="WB-MCC", Input="-2", Path="ch=='-' && unary => TT", Expected="u- 2" },

        // ===== ЗАДАНИЕ 2: EvaluatePostfix =====
        new() { Id="T2-EC1", Task="Task2", Method="EC", Input="2 3 +", Path="бинарный плюс", Expected="5" },
        new() { Id="T2-EC2", Task="Task2", Method="EC", Input="2 u-", Path="унарный минус", Expected="-2" },
        new() { Id="T2-EC3", Task="Task2", Method="EC", Input="5 0 /", Path="деление на ноль", Expected="ERROR: Деление на ноль" },
        new() { Id="T2-EC4", Task="Task2", Method="EC", Input="2 +", Path="мало операндов", Expected="ERROR: Недостаточно операндов для оператора +" },

        new() { Id="T2-BV1", Task="Task2", Method="BV", Input="", Path="пустой ввод", Expected="0" },
        new() { Id="T2-BV2", Task="Task2", Method="BV", Input="7", Path="один токен", Expected="7" },
        new() { Id="T2-BV3", Task="Task2", Method="BV", Input="2 -1 ^", Path="граница степени", Expected="ERROR: Отрицательная степень не поддерживается" },

        new() { Id="T2-CE1", Task="Task2", Method="CE", Input="2 3 ?", Path="unknown op", Expected="ERROR: Неизвестный оператор: ?" },
        new() { Id="T2-CE2", Task="Task2", Method="CE", Input="u-", Path="u- без операнда", Expected="ERROR: Недостаточно операндов для унарного минуса" },

        new() { Id="T2-EG1", Task="Task2", Method="EG", Input="2 3", Path="лишний операнд", Expected="3" },
        new() { Id="T2-EG2", Task="Task2", Method="EG", Input="10 2 mod", Path="оператор-слово", Expected="ERROR: Неизвестный оператор: mod" },

        // White-box (1..5)
        new() { Id="T2-WB1", Task="Task2", Method="WB-S", Input="9", Path="statement coverage", Expected="9" },
        new() { Id="T2-WB2", Task="Task2", Method="WB-B", Input="3 u-", Path="ветвь token==u- true", Expected="-3" },
        new() { Id="T2-WB3", Task="Task2", Method="WB-C", Input="u-", Path="условие ok=false", Expected="ERROR: Недостаточно операндов для унарного минуса" },
        new() { Id="T2-WB4", Task="Task2", Method="WB-DC", Input="4 0 /", Path="решения+условия / b==0", Expected="ERROR: Деление на ноль" },
        new() { Id="T2-WB5", Task="Task2", Method="WB-MCC", Input="2 -2 ^", Path="^ и b<0", Expected="ERROR: Отрицательная степень не поддерживается" },
    };

    public static List<TestResult> RunAll()
    {
        var all = BuildAll();
        var results = new List<TestResult>();

        foreach (var t in all)
        {
            string actual = t.Task == "Task1"
                ? NativeMethods.ConvertToPoliz(t.Input)
                : NativeMethods.CalculatePostfix(t.Input);

            results.Add(new TestResult { Case = t, Actual = actual });
        }

        return results;
    }

    public static void PrintTable(IEnumerable<TestResult> data, string title)
        => PrintTable(data, title, data);

    private static void PrintTable(IEnumerable<TestResult> data, string title, IEnumerable<TestResult> widthSource)
    {
        var rows = data.ToList();

        const string inputHeader = "Значения исходных данных";
        const string pathHeader = "Путь";
        const string expectedHeader = "Ожидаемый результат";
        const string actualHeader = "Фактический результат";
        const string verdictHeader = "Результат тестирования";

        var widths = GetTableWidths(widthSource, inputHeader, pathHeader, expectedHeader, actualHeader, verdictHeader);

        var separator = $"+-{new string('-', widths.Input)}-+-{new string('-', widths.Path)}-+-{new string('-', widths.Expected)}-+-{new string('-', widths.Actual)}-+-{new string('-', widths.Verdict)}-+";

        Console.WriteLine($"\n{title}");
        Console.WriteLine(separator);
        Console.WriteLine($"| {inputHeader.PadRight(widths.Input)} | {pathHeader.PadRight(widths.Path)} | {expectedHeader.PadRight(widths.Expected)} | {actualHeader.PadRight(widths.Actual)} | {verdictHeader.PadRight(widths.Verdict)} |");
        Console.WriteLine(separator);

        foreach (var r in rows)
        {
            Console.WriteLine($"| {r.Case.Input.PadRight(widths.Input)} | {r.Case.Path.PadRight(widths.Path)} | {r.Case.Expected.PadRight(widths.Expected)} | {r.Actual.PadRight(widths.Actual)} | {r.Verdict.PadRight(widths.Verdict)} |");
        }

        Console.WriteLine(separator);
    }

    public static void PrintByTaskAndMethod(List<TestResult> results)
    {
        var grouped = results
            .GroupBy(r => new { r.Case.Task, r.Case.Method })
            .OrderBy(g => g.Key.Task)
            .ThenBy(g => g.Key.Method);

        foreach (var g in grouped)
        {
            PrintTable(g, $"{g.Key.Task} / Метод: {g.Key.Method}", results);
        }
    }

    private static TableWidths GetTableWidths(
        IEnumerable<TestResult> source,
        string inputHeader,
        string pathHeader,
        string expectedHeader,
        string actualHeader,
        string verdictHeader)
    {
        var rows = source.ToList();

        return new TableWidths
        {
            Input = Math.Max(inputHeader.Length, rows.Select(r => r.Case.Input.Length).DefaultIfEmpty(0).Max()),
            Path = Math.Max(pathHeader.Length, rows.Select(r => r.Case.Path.Length).DefaultIfEmpty(0).Max()),
            Expected = Math.Max(expectedHeader.Length, rows.Select(r => r.Case.Expected.Length).DefaultIfEmpty(0).Max()),
            Actual = Math.Max(actualHeader.Length, rows.Select(r => r.Actual.Length).DefaultIfEmpty(0).Max()),
            Verdict = Math.Max(verdictHeader.Length, rows.Select(r => r.Verdict.Length).DefaultIfEmpty(0).Max())
        };
    }
}