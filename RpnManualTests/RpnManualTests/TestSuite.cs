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
        // =========================================================================
        // ЗАДАНИЕ 1: ToPoliz (Перевод в ПОЛИЗ)
        // =========================================================================
        
        // --- Чёрный ящик ---
        new() { Id="T1-EC1", Task="Задание 1", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="2+3", Path="-", Expected="2 3 +" },
        new() { Id="T1-EC2", Task="Задание 1", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="(2+3)*4", Path="-", Expected="2 3 + 4 *" },
        new() { Id="T1-EC3", Task="Задание 1", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="-2+3", Path="-", Expected="u- 2 3 +" },
        new() { Id="T1-EC4", Task="Задание 1", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="(-2)+3", Path="-", Expected="u- 2 3 +" },
        new() { Id="T1-EC5", Task="Задание 1", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="2+a", Path="-", Expected="ERROR: Не известный символ a" },
        new() { Id="T1-EC6", Task="Задание 1", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="1+2)", Path="-", Expected="ERROR: несогласованные скобки" },
        new() { Id="T1-EC7", Task="Задание 1", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="(1+2", Path="-", Expected="ERROR: несогласованные скобки" },

        new() { Id="T1-BV1", Task="Задание 1", BoxType=TestType.BlackBox, Method="Анализ граничных значений", Input="", Path="-", Expected="" },
        new() { Id="T1-BV2", Task="Задание 1", BoxType=TestType.BlackBox, Method="Анализ граничных значений", Input="7", Path="-", Expected="7" },
        new() { Id="T1-BV3", Task="Задание 1", BoxType=TestType.BlackBox, Method="Анализ граничных значений", Input=")", Path="-", Expected="ERROR: несогласованные скобки" },
        new() { Id="T1-BV4", Task="Задание 1", BoxType=TestType.BlackBox, Method="Анализ граничных значений", Input="1+2*3", Path="-", Expected="1 2 3 * +" },

        new() { Id="T1-CE1", Task="Задание 1", BoxType=TestType.BlackBox, Method="Причинно-следственные связи", Input="1+2*3", Path="-", Expected="1 2 3 * +" },
        new() { Id="T1-CE2", Task="Задание 1", BoxType=TestType.BlackBox, Method="Причинно-следственные связи", Input="-2+3", Path="-", Expected="u- 2 3 +" },
        new() { Id="T1-CE3", Task="Задание 1", BoxType=TestType.BlackBox, Method="Причинно-следственные связи", Input="2+a", Path="-", Expected="ERROR: Не известный символ a" },
        new() { Id="T1-CE4", Task="Задание 1", BoxType=TestType.BlackBox, Method="Причинно-следственные связи", Input="(1+2", Path="-", Expected="ERROR: несогласованные скобки" },

        new() { Id="T1-EG1", Task="Задание 1", BoxType=TestType.BlackBox, Method="Предположение об ошибке", Input="  12   +  3 ", Path="-", Expected="12 3 +" },
        new() { Id="T1-EG2", Task="Задание 1", BoxType=TestType.BlackBox, Method="Предположение об ошибке", Input="--2", Path="-", Expected="u- u- 2" },
        new() { Id="T1-EG3", Task="Задание 1", BoxType=TestType.BlackBox, Method="Предположение об ошибке", Input="2+3)", Path="-", Expected="ERROR: несогласованные скобки" },

        // --- Белый ящик ---
        new() { Id="T1-WB1", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="1+2", Path="A-B-D-E-F-B-L-M-O-P-B-D-E-F-B-Q-R-S-T", Expected="1 2 +" },
        new() { Id="T1-WB2", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="(1+2)*3", Path="A-B-G-H-B-D-E-F-B-L-M-O-P-B...T", Expected="1 2 + 3 *" },
        new() { Id="T1-WB3", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="-2+3", Path="A-B-L-M-N-B-D-E-F...T", Expected="u- 2 3 +" },
        new() { Id="T1-WB4", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="2+a", Path="A-B-D-E-F-B-L-X2", Expected="ERROR: Не известный символ a" },
        new() { Id="T1-WB5", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="1+2)", Path="A-B-D-E-F-B-L-M-O-P-B-D-E-F-B-I-J-K-X1", Expected="ERROR: несогласованные скобки" },
        new() { Id="T1-WB6", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="(1+2", Path="A-B-G-H-B-D...Q-R-S-X3", Expected="ERROR: несогласованные скобки" },

        new() { Id="T1-MCC1", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Комбинаторное покрытие (Метод 5)", Input="-2", Path="A-B-L-M(Да)-N...T", Expected="u- 2" },
        new() { Id="T1-MCC2", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Комбинаторное покрытие (Метод 5)", Input="2-1", Path="A-B-D-E-F-B-L-M(Нет)-O-P...T", Expected="2 1 -" },
        new() { Id="T1-MCC3", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Комбинаторное покрытие (Метод 5)", Input="+2", Path="A-B-L-M(Нет)-O-P-B-D-E-F...T", Expected="2 +" },
        new() { Id="T1-MCC4", Task="Задание 1", BoxType=TestType.WhiteBox, Method="Комбинаторное покрытие (Метод 5)", Input="2+1", Path="A-B-D-E-F-B-L-M(Нет)-O-P...T", Expected="2 1 +" },


        // =========================================================================
        // ЗАДАНИЕ 2: EvaluatePostfix (Вычисление ПОЛИЗ)
        // =========================================================================
        
        // --- Чёрный ящик ---
        new() { Id="T2-EC1", Task="Задание 2", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="2 3 +", Path="-", Expected="5" },
        new() { Id="T2-EC2", Task="Задание 2", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="2 u-", Path="-", Expected="-2" },
        new() { Id="T2-EC3", Task="Задание 2", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="2 +", Path="-", Expected="ERROR: Недостаточно операндов для оператора +" },
        new() { Id="T2-EC4", Task="Задание 2", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="u-", Path="-", Expected="ERROR: Недостаточно операндов для унарного минуса" },
        new() { Id="T2-EC5", Task="Задание 2", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="5 0 /", Path="-", Expected="ERROR: Деление на ноль" },
        new() { Id="T2-EC6", Task="Задание 2", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="2 3 ?", Path="-", Expected="ERROR: Неизвестный оператор: ?" },
        new() { Id="T2-EC7", Task="Задание 2", BoxType=TestType.BlackBox, Method="Разбиение на классы эквивалентности", Input="2 -1 ^", Path="-", Expected="ERROR: Отрицательная степень не поддерживается" },

        new() { Id="T2-BV1", Task="Задание 2", BoxType=TestType.BlackBox, Method="Анализ граничных значений", Input="", Path="-", Expected="0" },
        new() { Id="T2-BV2", Task="Задание 2", BoxType=TestType.BlackBox, Method="Анализ граничных значений", Input="7", Path="-", Expected="7" },
        new() { Id="T2-BV3", Task="Задание 2", BoxType=TestType.BlackBox, Method="Анализ граничных значений", Input="1 1 /", Path="-", Expected="1" },
        new() { Id="T2-BV4", Task="Задание 2", BoxType=TestType.BlackBox, Method="Анализ граничных значений", Input="1 0 /", Path="-", Expected="ERROR: Деление на ноль" },

        new() { Id="T2-CE1", Task="Задание 2", BoxType=TestType.BlackBox, Method="Причинно-следственные связи", Input="3 u-", Path="-", Expected="-3" },
        new() { Id="T2-CE2", Task="Задание 2", BoxType=TestType.BlackBox, Method="Причинно-следственные связи", Input="2 3 +", Path="-", Expected="5" },
        new() { Id="T2-CE3", Task="Задание 2", BoxType=TestType.BlackBox, Method="Причинно-следственные связи", Input="2 +", Path="-", Expected="ERROR: Недостаточно операндов для оператора +" },
        new() { Id="T2-CE4", Task="Задание 2", BoxType=TestType.BlackBox, Method="Причинно-следственные связи", Input="5 0 /", Path="-", Expected="ERROR: Деление на ноль" },

        new() { Id="T2-EG1", Task="Задание 2", BoxType=TestType.BlackBox, Method="Предположение об ошибке", Input=" 12   3  + ", Path="-", Expected="15" },
        new() { Id="T2-EG2", Task="Задание 2", BoxType=TestType.BlackBox, Method="Предположение об ошибке", Input="2 3", Path="-", Expected="3" },
        new() { Id="T2-EG3", Task="Задание 2", BoxType=TestType.BlackBox, Method="Предположение об ошибке", Input="10 2 mod", Path="-", Expected="ERROR: Неизвестный оператор: mod" },

        // --- Белый ящик ---
        new() { Id="T2-WB1", Task="Задание 2", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="9", Path="A-B-C-D-E-F-G-E-Z1", Expected="9" },
        new() { Id="T2-WB2", Task="Задание 2", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="3 u-", Path="A-B-C-D-E-F-G-E-F-H-I-J-E-Z1", Expected="-3" },
        new() { Id="T2-WB3", Task="Задание 2", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="u-", Path="A-B-C-D-E-F-H-I-X1", Expected="ERROR: Недостаточно операндов для унарного минуса" },
        new() { Id="T2-WB4", Task="Задание 2", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="+", Path="A-B-C-D-E-F-H-K-X2", Expected="ERROR: Недостаточно операндов для оператора +" },
        new() { Id="T2-WB5", Task="Задание 2", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="4 0 /", Path="A-B-C-D-E-F-G...H-K-L-M-Q-X3", Expected="ERROR: Деление на ноль" },
        new() { Id="T2-WB6", Task="Задание 2", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="2 -2 ^", Path="A-B-C-D-E-F-G...H-K-L-M-S-X4", Expected="ERROR: Отрицательная степень не поддерживается" },
        new() { Id="T2-WB7", Task="Задание 2", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="2 3 @", Path="A-B-C-D-E-F-G...H-K-L-M-X5", Expected="ERROR: Неизвестный оператор: @" },
        new() { Id="T2-WB8", Task="Задание 2", BoxType=TestType.WhiteBox, Method="Структурное покрытие (Методы 1-4)", Input="", Path="A-B-C-Z0", Expected="0" },
    };

    public static List<TestResult> RunAll()
    {
        var all = BuildAll();
        var results = new List<TestResult>();

        foreach (var t in all)
        {
            string actual = t.Task == "Задание 1"
                ? NativeMethods.ConvertToPoliz(t.Input)
                : NativeMethods.CalculatePostfix(t.Input);

            results.Add(new TestResult { Case = t, Actual = actual });
        }

        return results;
    }

    public static void PrintByTaskAndMethod(List<TestResult> results)
    {
        var groupedByTask = results.GroupBy(r => r.Case.Task).OrderBy(g => g.Key);

        foreach (var taskGroup in groupedByTask)
        {
            Console.WriteLine($"\n========================================================================================================================");
            Console.WriteLine($"                                                 {taskGroup.Key.ToUpper()}");
            Console.WriteLine($"========================================================================================================================");

            var groupedByBox = taskGroup.GroupBy(r => r.Case.BoxType).OrderBy(g => g.Key);

            foreach (var boxGroup in groupedByBox)
            {
                string boxName = boxGroup.Key == TestType.BlackBox
                    ? "ЧЁРНЫЙ ЯЩИК (Функциональное тестирование)"
                    : "БЕЛЫЙ ЯЩИК (Структурное тестирование)";

                Console.WriteLine($"\n>>> {boxName} <<<\n");

                var groupedByMethod = boxGroup.GroupBy(r => r.Case.Method);
                foreach (var methodGroup in groupedByMethod)
                {
                    // Для белого ящика показываем колонку "Путь", для чёрного - скрываем.
                    bool showPath = boxGroup.Key == TestType.WhiteBox;
                    PrintTable(methodGroup, $"Метод: {methodGroup.Key}", methodGroup, showPath);
                }
            }
        }
    }

    private static void PrintTable(IEnumerable<TestResult> data, string title, IEnumerable<TestResult> widthSource, bool showPath)
    {
        var rows = data.ToList();

        const string inputHeader = "Входные данные";
        const string pathHeader = "Путь по блок-схеме";
        const string expectedHeader = "Ожидаемый результат";
        const string actualHeader = "Фактический результат";
        const string verdictHeader = "Статус";

        var widths = GetTableWidths(widthSource, inputHeader, pathHeader, expectedHeader, actualHeader, verdictHeader);

        string separator = showPath
            ? $"+-{new string('-', widths.Input)}-+-{new string('-', widths.Path)}-+-{new string('-', widths.Expected)}-+-{new string('-', widths.Actual)}-+-{new string('-', widths.Verdict)}-+"
            : $"+-{new string('-', widths.Input)}-+-{new string('-', widths.Expected)}-+-{new string('-', widths.Actual)}-+-{new string('-', widths.Verdict)}-+";

        Console.WriteLine(title);
        Console.WriteLine(separator);

        if (showPath)
        {
            Console.WriteLine($"| {inputHeader.PadRight(widths.Input)} | {pathHeader.PadRight(widths.Path)} | {expectedHeader.PadRight(widths.Expected)} | {actualHeader.PadRight(widths.Actual)} | {verdictHeader.PadRight(widths.Verdict)} |");
        }
        else
        {
            Console.WriteLine($"| {inputHeader.PadRight(widths.Input)} | {expectedHeader.PadRight(widths.Expected)} | {actualHeader.PadRight(widths.Actual)} | {verdictHeader.PadRight(widths.Verdict)} |");
        }

        Console.WriteLine(separator);

        foreach (var r in rows)
        {
            if (showPath)
            {
                Console.WriteLine($"| {r.Case.Input.PadRight(widths.Input)} | {r.Case.Path.PadRight(widths.Path)} | {r.Case.Expected.PadRight(widths.Expected)} | {r.Actual.PadRight(widths.Actual)} | {r.Verdict.PadRight(widths.Verdict)} |");
            }
            else
            {
                Console.WriteLine($"| {r.Case.Input.PadRight(widths.Input)} | {r.Case.Expected.PadRight(widths.Expected)} | {r.Actual.PadRight(widths.Actual)} | {r.Verdict.PadRight(widths.Verdict)} |");
            }
        }

        Console.WriteLine(separator + "\n");
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
            Input = Math.Max(inputHeader.Length, rows.Select(r => r.Case.Input?.Length ?? 0).DefaultIfEmpty(0).Max()),
            Path = Math.Max(pathHeader.Length, rows.Select(r => r.Case.Path?.Length ?? 0).DefaultIfEmpty(0).Max()),
            Expected = Math.Max(expectedHeader.Length, rows.Select(r => r.Case.Expected?.Length ?? 0).DefaultIfEmpty(0).Max()),
            Actual = Math.Max(actualHeader.Length, rows.Select(r => r.Actual?.Length ?? 0).DefaultIfEmpty(0).Max()),
            Verdict = Math.Max(verdictHeader.Length, rows.Select(r => r.Verdict?.Length ?? 0).DefaultIfEmpty(0).Max())
        };
    }
}