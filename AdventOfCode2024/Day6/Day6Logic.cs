using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day6
{
    public class Day6Logic : IPuzzles
    {
        private const string fileName = "day6_Input.in";
        private const Int32 BufferSize = 128;
        private List<string> map = [];
        private Point guardStartingPosition = new();
        private HashSet<(Point, Point)> visitedPointsP1 = [];

        private readonly List<Point> moves = [new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0)];

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

            visitedPointsP1 = Move(map);

            return visitedPointsP1.DistinctBy(x => x.Item1).Count().ToString();
        }

        public string SecondPuzzle()
        {
            int result = 0;

            foreach (var point in visitedPointsP1.DistinctBy(x => x.Item1))
            {
                if (point.Item1.X == guardStartingPosition.X && point.Item1.Y == guardStartingPosition.Y)
                {
                    continue;
                }

                var mapWithNewObsticleAdded = new List<string>(map);

                var sb = new StringBuilder(mapWithNewObsticleAdded[point.Item1.Y]);
                sb[point.Item1.X] = '#';
                mapWithNewObsticleAdded[point.Item1.Y] = sb.ToString();

                var visitedPoints = Move(mapWithNewObsticleAdded);

                if(visitedPoints.Count == 0)
                {
                    result++;
                }
            }

            return result.ToString();
        }

        private HashSet<(Point, Point)> Move(List<string> map)
        {
            HashSet<(Point, Point)> visitedPoints = [];

            var turnsRight = 0;

            var currentDirection = new Point(moves[turnsRight].X, moves[turnsRight].Y);
            var currentPosition = new Point(guardStartingPosition.X, guardStartingPosition.Y);

            visitedPoints.Add((currentPosition, currentDirection));

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

                    var addSuccessful = visitedPoints.Add((newPosition, currentDirection));
                    if(!addSuccessful)
                    {
                        return [];
                    }
                    currentPosition = newPosition;
                }
            }

            return visitedPoints;
        }

        private bool PointIsWithinMap(Point point)
        {
            if(point.X >= 0 && point.Y >= 0 && point.X < map[0].Length && point.Y < map.Count)
            {
                return true;
            }

            return false;
        }

        private record Point
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
    }
}