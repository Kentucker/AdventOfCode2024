using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day12
{
    public class Day12Logic : IPuzzles
    {
        private const string fileName = "day12_Input.in";
        private const Int32 BufferSize = 128;

        public string FirstPuzzle()
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
}