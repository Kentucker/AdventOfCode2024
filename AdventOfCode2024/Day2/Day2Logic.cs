using AdventOfCode2024.Interfaces;
using System.Reflection.Emit;
using System.Text;

namespace AdventOfCode2024.Day2
{
    public class Day2Logic : IPuzzles
    {
        private const string fileName = "day2_Input.in";
        private const Int32 BufferSize = 128;

        public string FirstPuzzle()
        {
            int result = 0;

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? report;
                while ((report = streamReader.ReadLine()) != null)
                {
                    var levels = report.Split(' ').ToList();

                    var checkIfSafe = EvaluateReport(levels);

                    if (checkIfSafe)
                    {
                        result++;
                    }
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
                string? report;
                while ((report = streamReader.ReadLine()) != null)
                {
                    var levels = report.Split(' ').ToList();

                    var checkIfSafe = EvaluateReport(levels);

                    if (checkIfSafe)
                    {
                        result++;
                    }
                    else
                    {
                        for (int i = 0; i < levels.Count; i++)
                        {
                            var copyLevels = new List<string>(levels);
                            copyLevels.RemoveAt(i);

                            checkIfSafe = EvaluateReport(copyLevels);

                            if (checkIfSafe)
                            {
                                result++;
                                break;
                            }
                        }
                    }
                }
            }

            return result.ToString();
        }

        private bool EvaluateReport(List<string> levels)
        {
            var asc = true;
            if (int.Parse(levels[1]) - int.Parse(levels[0]) < 0)
            {
                asc = false;
            }

            for (int i = 0; i < levels.Count - 1; i++)
            {
                var diff = int.Parse(levels[i + 1]) - int.Parse(levels[i]);
                var wrongDiff = Math.Abs(diff) < 1 || Math.Abs(diff) > 3;
                var notOrdered = (asc == true && diff < 0) || (asc == false && diff > 0);

                if (wrongDiff || notOrdered)
                {
                    return false;
                }

            }

            return true;
        }
    }
}