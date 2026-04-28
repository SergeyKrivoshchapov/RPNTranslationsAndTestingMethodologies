using System;
using RpnManualTests;

Console.WindowWidth = Math.Max(Console.WindowWidth, 150); // Делаем консоль пошире для таблиц

var results = TestSuite.RunAll();

// Печатаем таблицы с группировкой по заданиям и ящикам
TestSuite.PrintByTaskAndMethod(results);

Console.WriteLine("\nГотово. Нажмите любую клавишу для выхода...");
Console.ReadLine();