using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day7
{
    public class Day7Logic : IPuzzles
    {
        private const string fileName = "day7_Input.in";
        private const Int32 BufferSize = 128;

        public string FirstPuzzle()
        {
            long result = 0;

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var parts = line.Split(':');
                    long testValue = long.Parse(parts[0].Trim());
                    var numbers = Array.ConvertAll(parts[1].Trim().Split(' '), long.Parse);

                    if (CanFormTestValue(numbers, 0, numbers[0], testValue))
                    {
                        result += testValue;
                    }
                }
            }

            return result.ToString();
        }

        public string SecondPuzzle()
        {
            long result = 0;

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var parts = line.Split(':');
                    var testValue = long.Parse(parts[0].Trim());
                    var numbers = Array.ConvertAll(parts[1].Trim().Split(' '), long.Parse);

                    if (CanFormTestValue(numbers, 0, numbers[0], testValue, true))
                    {
                        result += testValue;
                    }
                }
            }

            return result.ToString();
        }

        private static bool CanFormTestValue(long[] numbers, int index, long currentValue, long target, bool concat = false)
        {
            if (index == numbers.Length - 1)
            {
                return currentValue == target;
            }

            if (CanFormTestValue(numbers, index+1, currentValue + numbers[index+1], target, concat))
            {
                return true;
            }

            if (CanFormTestValue(numbers, index+1, currentValue * numbers[index+1], target, concat))
            {
                return true;
            }

            if (concat)
            {
                var concatenatedValue = long.Parse($"{currentValue}{numbers[index+1]}");

                if (CanFormTestValue(numbers, index+1, concatenatedValue, target, concat))
                {
                    return true;
                }
            }

            return false;
        }
    }
}