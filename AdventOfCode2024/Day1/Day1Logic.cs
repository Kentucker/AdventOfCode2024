using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day1
{
    public class Day1Logic : IPuzzles
    {
        private const string fileName = "day1_Input.in";
        private const Int32 BufferSize = 128;

        public string FirstPuzzle()
        {
            int result = 0;

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                var firstList = new List<int>();
                var secondList = new List<int>();

                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var numbers = line.Split("   ");
                    firstList.Add(int.Parse(numbers[0]));
                    secondList.Add(int.Parse(numbers[1]));
                }

                firstList.Sort();
                secondList.Sort();

                var pairs = firstList.Zip(secondList, (fn, sn) => new { FirstNumber = fn, SecondNumber = sn });

                foreach (var item in pairs)
                {
                    result += Math.Abs(item.FirstNumber - item.SecondNumber);
                }
            }

            return result.ToString();
        }

        public string SecondPuzzle()
        {
            int result = 0;

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                var firstList = new List<int>();
                var secondList = new List<int>();

                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var numbers = line.Split("   ");

                    firstList.Add(int.Parse(numbers[0]));
                    secondList.Add(int.Parse(numbers[1]));
                }

                foreach (var item in firstList)
                {
                    var count = secondList.Count(x => x == item);
                    result += item * count;
                }
            }

            return result.ToString();
        }
    }
}