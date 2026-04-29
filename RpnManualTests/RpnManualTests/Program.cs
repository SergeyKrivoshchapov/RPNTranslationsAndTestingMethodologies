using System;
using RpnManualTests;

Console.WindowWidth = Math.Max(Console.WindowWidth, 150);

var results = TestSuite.RunAll();

TestSuite.PrintByTaskAndMethod(results);

Console.WriteLine("\nГотово. Нажмите любую клавишу для выхода...");
Console.ReadLine();