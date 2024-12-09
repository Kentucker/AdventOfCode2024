using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day8
{
    public class Day8Logic : IPuzzles
    {
        private const string fileName = "day8_Input.in";
        private const Int32 BufferSize = 128;
        private List<(char, Point)> antennas = [];
        private Point mapSize = new();

        public string FirstPuzzle()
        {
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                var lineNumber = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    mapSize = new Point(lineNumber, line.Length - 1);

                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] != '.')
                        {
                            antennas.Add((line[i], new Point(lineNumber, i)));
                        }
                    }

                    lineNumber++;
                }
            }

            var antinodes = SearchForAntinodes(antennas, mapSize);

            return antinodes.Count.ToString();
        }

        public string SecondPuzzle()
        {
            var antinodes = SearchForAntinodesAtAnyGridPosition(antennas, mapSize);

            return antinodes.Count.ToString();
        }

        private static HashSet<Point> SearchForAntinodes(List<(char, Point)> antennas, Point maxMapSize)
        {
            var antinodes = new HashSet<Point>();

            var distinctAntennasTypes = antennas.Select(x => x.Item1).Distinct();

            foreach (var antennaType in distinctAntennasTypes)
            {
                var antennasOfASingleTypePositions = antennas.Where(x => x.Item1 == antennaType).Select(x => x.Item2).ToList();

                foreach (var antenna in antennasOfASingleTypePositions)
                {
                    for (int i = 0; i < antennasOfASingleTypePositions.Count - 1; i++)
                    {
                        for (int j = i + 1; j < antennasOfASingleTypePositions.Count; j++)
                        {
                            var diffX = antennasOfASingleTypePositions[i].X - antennasOfASingleTypePositions[j].X;
                            var diffY = antennasOfASingleTypePositions[i].Y - antennasOfASingleTypePositions[j].Y;

                            var vector = new Point(diffX, diffY);

                            var point1 = new Point(
                                antennasOfASingleTypePositions[i].X + vector.X,
                                antennasOfASingleTypePositions[i].Y + vector.Y
                                );

                            if (IsPointInMap(point1, maxMapSize))
                            {
                                antinodes.Add(point1);
                            }

                            var point2 = new Point(
                                antennasOfASingleTypePositions[j].X - vector.X,
                                antennasOfASingleTypePositions[j].Y - vector.Y
                                );

                            if (IsPointInMap(point2, maxMapSize))
                            {
                                antinodes.Add(point2);
                            }
                        }
                    }
                }
            }

            return antinodes;
        }

        private static HashSet<Point> SearchForAntinodesAtAnyGridPosition(List<(char, Point)> antennas, Point maxMapSize)
        {
            var antinodes = new HashSet<Point>();

            var distinctAntennasTypes = antennas.Select(x => x.Item1).Distinct();

            foreach (var antennaType in distinctAntennasTypes)
            {
                var antennasOfASingleTypePositions = antennas.Where(x => x.Item1 == antennaType).Select(x => x.Item2).ToList();

                foreach (var antenna in antennasOfASingleTypePositions)
                {
                    for (int i = 0; i < antennasOfASingleTypePositions.Count - 1; i++)
                    {
                        for (int j = i + 1; j < antennasOfASingleTypePositions.Count; j++)
                        {
                            var diffX = antennasOfASingleTypePositions[i].X - antennasOfASingleTypePositions[j].X;
                            var diffY = antennasOfASingleTypePositions[i].Y - antennasOfASingleTypePositions[j].Y;

                            var vector = new Point(diffX, diffY);

                            var currentPoint = antennasOfASingleTypePositions[i];

                            while (IsPointInMap(currentPoint, maxMapSize))
                            {
                                antinodes.Add(currentPoint);

                                currentPoint = new Point(
                                    currentPoint.X + vector.X,
                                    currentPoint.Y + vector.Y
                                );
                            }

                            currentPoint = antennasOfASingleTypePositions[j];

                            while (IsPointInMap(currentPoint, maxMapSize))
                            {
                                antinodes.Add(currentPoint);

                                currentPoint = new Point(
                                    currentPoint.X - vector.X,
                                    currentPoint.Y - vector.Y
                                );
                            }
                        }
                    }
                }
            }

            return antinodes;
        }

        private static bool IsPointInMap(Point point, Point maxMapSize)
        {
            if (point.X >= 0 && point.X <= maxMapSize.X && point.Y >= 0 && point.Y <= maxMapSize.Y)
            {
                return true;
            }

            return false;
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