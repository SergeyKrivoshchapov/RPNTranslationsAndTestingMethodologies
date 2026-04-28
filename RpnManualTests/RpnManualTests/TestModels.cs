namespace RpnManualTests;

public sealed class TestCase
{
    public string Id { get; init; } = "";
    public string Task { get; init; } = "";    // Task1/Task2
    public string Method { get; init; } = "";  // EC/BV/CE/EG/WB-S/WB-B/WB-C/WB-DC/WB-MCC
    public string Input { get; init; } = "";
    public string Path { get; init; } = "";
    public string Expected { get; init; } = "";
}

public sealed class TestResult
{
    public TestCase Case { get; init; } = new();
    public string Actual { get; init; } = "";
    public string Verdict => Actual == Case.Expected ? "Успешно" : "Неуспешно";
}