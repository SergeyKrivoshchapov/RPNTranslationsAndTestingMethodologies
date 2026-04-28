using RpnManualTests;

var results = TestSuite.RunAll();

// Общая таблица
TestSuite.PrintTable(results, "Общая таблица тестов (оба задания)");

// Таблицы по каждому методу
TestSuite.PrintByTaskAndMethod(results);

Console.WriteLine("\nГотово.");