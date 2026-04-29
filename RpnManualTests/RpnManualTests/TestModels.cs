namespace RpnManualTests;

public enum TestType
{
    BlackBox,
    WhiteBox
}

public sealed class TestCase
{
    public string Id { get; init; } = "";
    public string Task { get; init; } = "";
    public TestType BoxType { get; init; }
    public string Method { get; init; } = "";
    public string Input { get; init; } = "";
    public string Path { get; init; } = "";
    public string Expected { get; init; } = "";
}

public sealed class TestResult
{
    public TestCase Case { get; init; } = new();
    public string Actual { get; init; } = "";
    public string Verdict => Actual == Case.Expected ? "Неуспешно" : "Успешно";
}