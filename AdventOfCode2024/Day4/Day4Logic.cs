using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day4
{
    public class Day4Logic : IPuzzles
    {
        private const string fileName = "day4_Input.in";
        private const Int32 BufferSize = 128;
        private List<string> wordSearch = [];
        private const string xmas = "XMAS";
        private const string max = "MAS";

        public string FirstPuzzle()
        {
            int result = 0;
            CreateWordSearch();

            for (int i = 0; i<wordSearch.Count; i++)
            {
                for (int j = 0; j < wordSearch[i].Length; j++)
                {
                    if (wordSearch[i][j] == 'X')
                    {
                        result += HowManyXmasesAround(new Point(i,j));
                    }
                }
            }

            return result.ToString();
        }

        public string SecondPuzzle()
        {
            int result = 0;

            for (int i = 1; i < wordSearch.Count-1; i++)
            {
                for (int j = 1; j < wordSearch[i].Length-1; j++)
                {
                    if (wordSearch[i][j] == 'A')
                    {
                        result += IsXMas(new Point(i, j));
                    }
                }
            }

            return result.ToString();
        }

        private void CreateWordSearch()
        {
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    wordSearch.Add(line);
                }
            }
        }

        private int HowManyXmasesAround(Point point)
        {
            int xmasesAround = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if(i == 0 && j == 0)
                    {
                        continue;
                    }
                    else
                    {
                        if(CheckIfXmasInDirection(new Point(i,j), point))
                        {
                            xmasesAround++;
                        }
                    }
                }
            }

            return xmasesAround;
        }

        private bool CheckIfXmasInDirection(Point direction, Point point)
        {
            var currentPoint = point;
            for (int i = 1; i < xmas.Length; i++)
            {
                currentPoint.X += direction.X;
                currentPoint.Y += direction.Y;

                if (!PointIsWithinWordSearch(currentPoint) || wordSearch[currentPoint.X][currentPoint.Y] != xmas[i])
                {
                    return false;
                }
            }

            return true;
        }

        private bool PointIsWithinWordSearch(Point point)
        {
            if(point.X >=0 && point.X < wordSearch.Count && point.Y >=0 && point.Y < wordSearch[0].Length)
            {
                return true;
            }

            return false;
        }

        private int IsXMas(Point point)
        {
            if (((wordSearch[point.X - 1][point.Y - 1] == 'M' && wordSearch[point.X + 1][point.Y + 1] == 'S') ||
                (wordSearch[point.X + 1][point.Y + 1] == 'M' && wordSearch[point.X - 1][point.Y - 1] == 'S')) &&
                ((wordSearch[point.X - 1][point.Y + 1] == 'M' && wordSearch[point.X + 1][point.Y - 1] == 'S') ||
                (wordSearch[point.X + 1][point.Y - 1] == 'M' && wordSearch[point.X - 1][point.Y + 1] == 'S')))
            {
                return 1;
            }

            return 0;
        }

        private struct Point(int x, int y)
        {
            public int X = x;
            public int Y = y;
        }
    }
}