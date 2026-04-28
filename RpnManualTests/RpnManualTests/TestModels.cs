namespace RpnManualTests;

public enum TestType
{
    BlackBox,
    WhiteBox
}

public sealed class TestCase
{
    public string Id { get; init; } = "";
    public string Task { get; init; } = "";    // "Задание 1" / "Задание 2"
    public TestType BoxType { get; init; }     // BlackBox / WhiteBox
    public string Method { get; init; } = "";  // "Классы эквивалентности", "Покрытие путей" и т.д.
    public string Input { get; init; } = "";
    public string Path { get; init; } = "";    // Путь по блок-схеме (для белого ящика)
    public string Expected { get; init; } = "";
}

public sealed class TestResult
{
    public TestCase Case { get; init; } = new();
    public string Actual { get; init; } = "";
    public string Verdict => Actual == Case.Expected ? "Успешно" : "Неуспешно";
}