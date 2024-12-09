using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day9
{
    public class Day9Logic : IPuzzles
    {
        private const string fileName = "day9_Input.in";
        private const Int32 BufferSize = 128;
        private List<int> individualBlocks = [];

        public string FirstPuzzle()
        {
            long checksum = 0;

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                var ID = 0;

                while ((line = streamReader.ReadLine()) != null)
                {
                    

                    for (int i = 0; i < line.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            individualBlocks.AddRange(Enumerable.Repeat(ID, int.Parse(line[i].ToString())));
                            ID++;
                        }
                        else
                        {
                            individualBlocks.AddRange(Enumerable.Repeat(-1, int.Parse(line[i].ToString())));
                        }
                    }

                    var fileFragmentation = new List<int>(individualBlocks);
                    fileFragmentation.FileSystemFragmentation();

                    var index = 0;

                    while(fileFragmentation[index] != -1)
                    {
                        checksum += fileFragmentation[index] * index;
                        index++;
                    }
                }
            }

            return checksum.ToString();
        }

        public string SecondPuzzle()
        {
            int result = 0;

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {

                }
            }

            return result.ToString();
        }
    }

    public static class StringExtensions
    {
        private static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public static void FileSystemFragmentation(this List<int> individualBlocks)
        {
            var indexOfFirstDotOccurence = individualBlocks.IndexOf(-1);
            while (!individualBlocks.Skip(indexOfFirstDotOccurence).All(n => n == -1))
            {
                var indexOfLastNumOccurence = individualBlocks.LastIndexOf(individualBlocks.LastOrDefault(x => x != -1));
                Swap(individualBlocks, indexOfFirstDotOccurence, indexOfLastNumOccurence);
                indexOfFirstDotOccurence = individualBlocks.IndexOf(-1);
            }
        }
    }
}