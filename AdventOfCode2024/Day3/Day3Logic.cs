using AdventOfCode2024.Interfaces;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day3
{
    public class Day3Logic : IPuzzles
    {
        private const string fileName = "day3_Input.in";
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
                    var patternMul = "mul\\((\\d{1,3}),(\\d{1,3})\\)";
                    var patternNumber = "\\d{1,3}";

                    foreach (Match match in Regex.Matches(line, patternMul,
                                               RegexOptions.None,
                                               TimeSpan.FromSeconds(1)))
                    {
                        var addend = (long)1;

                        foreach (Match match2 in Regex.Matches(match.Value, patternNumber,
                                               RegexOptions.None,
                                               TimeSpan.FromSeconds(1)))
                        {
                            addend *= long.Parse(match2.Value);
                        }

                        result += addend;
                    }
                }
            }

            return result.ToString();
        }

        public string SecondPuzzle()
        {
            long result = 0;
            var flag = true;

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var patternMul = "(mul\\(\\d{1,3},\\d{1,3}\\)|do\\(\\)|don't\\(\\))";
                    var patternNumber = "\\d{1,3}";
                    
                    foreach (Match match in Regex.Matches(line, patternMul,
                                               RegexOptions.NonBacktracking,
                                               TimeSpan.FromSeconds(1)))
                    {
                        if (match.Value == "don't()")
                        {
                            flag = false;
                            continue;
                        } 
                        else if (match.Value == "do()")
                        {
                            flag = true;
                            continue;
                        }
                        
                        if (flag == true)
                        {
                            var addend = (long)1;

                            foreach (Match match2 in Regex.Matches(match.Value, patternNumber,
                                                   RegexOptions.None,
                                                   TimeSpan.FromSeconds(1)))
                            {
                                addend *= long.Parse(match2.Value);
                            }

                            result += addend;
                        }
                    }
                }
            }

            return result.ToString();
        }
    }
}