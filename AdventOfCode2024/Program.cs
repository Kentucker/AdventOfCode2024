﻿using AdventOfCode2024.Day11;
using System.Diagnostics;

var logic = new Day11Logic();

var stopWatch = new Stopwatch();
stopWatch.Start();

var firstResult = logic.FirstPuzzle();
var secondResult = logic.SecondPuzzle();

stopWatch.Stop();

Console.WriteLine($@"1: {firstResult}, 2: {secondResult}, execution time: {stopWatch.ElapsedMilliseconds}ms");