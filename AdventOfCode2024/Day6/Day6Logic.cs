using AdventOfCode2024.Interfaces;
using System;
using System.Text;

namespace AdventOfCode2024.Day6
{
    public class Day6Logic : IPuzzles
    {
        private const string fileName = "day6_Input.in";
        private const Int32 BufferSize = 128;
        private List<string> map = [];
        private Point guardStartingPosition = new();
        List<Point> visitedPoints = [];
        List<Point> visitedPointsUnique = [];
        private List<Point> moves = [new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0)];

        public string FirstPuzzle()
        {
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                var lineNumber = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    map.Add(line);

                    if(line.Contains('^'))
                    {
                        guardStartingPosition = new Point(line.IndexOf('^'), lineNumber);
                    }

                    lineNumber++;
                }
            }

            Move(map);

            visitedPointsUnique = visitedPoints.Distinct(new ProductComparer()).ToList();

            return visitedPointsUnique.Count().ToString();
        }

        public string SecondPuzzle()
        {
            int result = 0;

            foreach (var point in visitedPointsUnique)
            {
                if(point.X == guardStartingPosition.X && point.Y == guardStartingPosition.Y)
                {
                    continue;
                }

                var mapUpdated = new List<string>(map);

                StringBuilder sb = new StringBuilder(mapUpdated[point.Y]);
                sb[point.X] = '#';
                mapUpdated[point.Y] = sb.ToString();

                result += Move(mapUpdated);
            }

            return result.ToString();
        }

        private int Move(List<string> map)
        {
            var currentPosition = new Point(guardStartingPosition.X, guardStartingPosition.Y);
            visitedPoints.Add(currentPosition);

            var turnsRight = 0;
            var currentDirection = new Point(moves[turnsRight].X, moves[turnsRight].Y);
            var steps = 0;

            while (PointIsWithinMap(new Point(currentPosition.X + currentDirection.X, currentPosition.Y + currentDirection.Y)))
            {
                if(map[currentPosition.Y + currentDirection.Y].ElementAt(currentPosition.X + currentDirection.X) == '#')
                {
                    turnsRight++;
                    currentDirection = moves[turnsRight%4];
                }
                else
                {
                    var newPosition = new Point(currentPosition.X + currentDirection.X, currentPosition.Y + currentDirection.Y);

                    visitedPoints.Add(newPosition);
                    currentPosition = newPosition;
                }

                steps++;
                if(steps > map.Count * map[0].Length)
                {
                    return 1; // loop
                }
            }

            return 0; // no loop
        }

        private bool PointIsWithinMap(Point point)
        {
            if(point.X >= 0 && point.Y >= 0 && point.X < map[0].Length && point.Y < map.Count)
            {
                return true;
            }

            return false;
        }

        private class Point
        {
            public Point()
            {

            }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }

            
        }

        class ProductComparer : IEqualityComparer<Point>
        {
            public bool Equals(Point? x, Point? y)
            {

                if (ReferenceEquals(x, y)) return true;

                if (x is null || y is null)
                    return false;

                return x.X == y.X && x.Y == y.Y;
            }

            public int GetHashCode(Point point)
            {
                if (point is null) return 0;

                return HashCode.Combine(point.X, point.Y);
            }
        }
    }
}