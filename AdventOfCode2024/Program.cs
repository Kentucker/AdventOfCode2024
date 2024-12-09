using AdventOfCode2024.Day8;
using System.Diagnostics;

var logic = new Day8Logic();

var stopWatch = new Stopwatch();
stopWatch.Start();

var firstResult = logic.FirstPuzzle();
var secondResult = logic.SecondPuzzle();

stopWatch.Stop();

Console.WriteLine($@"1: {firstResult}, 2: {secondResult}, execution time: {stopWatch.ElapsedMilliseconds}ms");