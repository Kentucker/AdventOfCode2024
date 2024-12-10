using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day10
{
    public class Day10Logic : IPuzzles
    {
        private const string fileName = "day10_Input.in";
        private const Int32 BufferSize = 128;
        private List<string> map = [];
        private List<Point> trailheads = [];

        public string FirstPuzzle()
        {
            int result = 0;
            
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                var lineNumber = 0;

                while ((line = streamReader.ReadLine()) != null)
                {
                    map.Add(line);

                    for (int i = line.IndexOf('0'); i > -1; i = line.IndexOf('0', i + 1))
                    {
                        trailheads.Add(new Point(i, lineNumber));
                    }

                    lineNumber++;
                }
            }

            foreach (var trailhead in trailheads)
            {
                result += SearchAllTrailsFromHead(map, trailhead, trailhead, []);
            }

            return result.ToString();
        }

        public string SecondPuzzle()
        {
            int result = 0;

            foreach (var trailhead in trailheads)
            {
                result += SearchAllTrailsFromHeadWithRating(map, trailhead);
            }

            return result.ToString();
        }

        private int SearchAllTrailsFromHead(List<string> map, Point trailhead, Point nextHop, HashSet<(Point, Point)> trails, int stepValue = 0)
        {
            stepValue++;

            var pointNeighours = PointNeighbours(map, nextHop, stepValue);

            foreach (var point in pointNeighours)
            {
                if (stepValue == 9)
                {
                    trails.Add((trailhead, point));
                }

                SearchAllTrailsFromHead(map, trailhead, point, trails, stepValue);
            }

            return trails.Count;
        }

        private int SearchAllTrailsFromHeadWithRating(List<string> map, Point nextHop, int numberOftrails = 0, int stepValue = 0)
        {
            stepValue++;

            var pointNeighours = PointNeighbours(map, nextHop, stepValue);

            foreach (var point in pointNeighours)
            {
                if (stepValue == 9)
                {
                    numberOftrails++;
                }

                numberOftrails = Math.Max(numberOftrails, SearchAllTrailsFromHeadWithRating(map, point, numberOftrails, stepValue));
            }

            return numberOftrails;
        }

        private List<Point> PointNeighbours(List<string> map, Point point, int value)
        {
            var neighbours = new List<Point>();

            List<Point> directions =
            [
                new(0, -1),
                new(0, 1),
                new(-1, 0),
                new(1, 0)
            ];

            int rows = map.Count;
            int cols = map[0].Length;

            foreach (var direction in directions)
            {
                int newX = point.X + direction.X;
                int newY = point.Y + direction.Y;

                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && int.Parse(map[newY][newX].ToString()) == value)
                {
                    neighbours.Add(new Point(newX, newY));
                }
            }

            return neighbours;
        }

        private struct Point
        {
            public Point()
            {
            }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X;
            public int Y;
        }
    }
}