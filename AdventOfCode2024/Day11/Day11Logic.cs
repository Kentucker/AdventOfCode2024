using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day11
{
    public class Day11Logic : IPuzzles
    {
        private const string fileName = "day11_Input.in";
        private const int BufferSize = 128;

        public string FirstPuzzle()
        {
            var stoneCounts = new Dictionary<long, long>();

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                foreach (var stone in streamReader.ReadLine()?.Split(' ') ?? [])
                {
                    var num = long.Parse(stone);

                    if (!stoneCounts.TryGetValue(num, out long value))
                    {
                        value = 0;
                        stoneCounts[num] = value;
                    }

                    stoneCounts[num] = ++value;
                }
            }

            SimulateBlinks(stoneCounts, 25);

            return stoneCounts.Values.Sum().ToString();
        }

        public string SecondPuzzle()
        {
            var stoneCounts = new Dictionary<long, long>();

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                foreach (var stone in streamReader.ReadLine()?.Split(' ') ?? [])
                {
                    var num = long.Parse(stone);

                    if (!stoneCounts.TryGetValue(num, out long value))
                    {
                        value = 0;
                        stoneCounts[num] = value;
                    }

                    stoneCounts[num] = ++value;
                }
            }

            SimulateBlinks(stoneCounts, 75);

            return stoneCounts.Values.Sum().ToString();
        }

        private static void SimulateBlinks(Dictionary<long, long> stoneCounts, int numberOfBlinks)
        {
            for (int i = 0; i < numberOfBlinks; i++)
            {
                var nextStoneCounts = new Dictionary<long, long>();

                foreach (var (stone, count) in stoneCounts)
                {
                    if (stone == 0)
                    {
                        AddToDictionary(nextStoneCounts, 1, count);
                    }
                    else
                    {
                        var numDigits = CountDigits(stone);

                        if (numDigits % 2 == 0)
                        {
                            var divider = PowerOf10(numDigits / 2);

                            var left = stone / divider;
                            var right = stone % divider;

                            AddToDictionary(nextStoneCounts, left, count);
                            AddToDictionary(nextStoneCounts, right, count);
                        }
                        else
                        {
                            var newStone = stone * 2024;
                            AddToDictionary(nextStoneCounts, newStone, count);
                        }
                    }
                }

                stoneCounts.Clear();

                foreach (var stoneCount in nextStoneCounts)
                {
                    stoneCounts[stoneCount.Key] = stoneCount.Value;
                }
            }
        }

        private static void AddToDictionary(Dictionary<long, long> dict, long stone, long count)
        {
            if (dict.ContainsKey(stone))
            {
                dict[stone] += count;
            }
            else
            {
                dict[stone] = count;
            }
        }

        private static int CountDigits(long num)
        {
            if (num == 0)
            {
                return 1;
            }

            var result = Math.Floor(Math.Log10(num) + 1);

            return (int)result;
        }

        private static long PowerOf10(int exponent)
        {
            long result = 1;

            while (exponent > 0)
            {
                result *= 10;
                exponent--;
            }

            return result;
        }
    }
}