using AdventOfCode2024.Interfaces;
using System.Text;

namespace AdventOfCode2024.Day5
{
    public class Day5Logic : IPuzzles
    {
        private const string fileName = "day5_Input.in";
        private const Int32 BufferSize = 128;
        private List<string> pageOrderingRules = [];
        private List<string> updates = [];
        private List<string> updatesIncorrectOrder = [];

        public string FirstPuzzle()
        {
            int result = 0;
            CreateRulesAndUpdates();

            foreach (var update in updates)
            {
                var updateList = update.Split(',').ToList();

                if (CheckIfRightOrder(update))
                {
                    var middleIndex = (updateList.Count - 1) / 2;
                    result += int.Parse(updateList[middleIndex]);
                }
            }

            return result.ToString();
        }

        public string SecondPuzzle()
        {
            int result = 0;

            foreach (var update in updatesIncorrectOrder)
            {
                var updateList = update.Split(',').ToList();
                var localUpdate = update;

                while (!CheckIfRightOrder(localUpdate, false))
                {
                    foreach (var rule in pageOrderingRules)
                    {
                        var pagesRule = rule.Split('|');

                        var firstIndex = updateList.FindIndex(x => x == pagesRule[0]);
                        var secondIndex = updateList.FindIndex(x => x == pagesRule[1]);

                        if ((updateList.Contains(pagesRule[0]) && updateList.Contains(pagesRule[1])) && firstIndex > secondIndex)
                        {
                            Swap(updateList, firstIndex, secondIndex);
                            localUpdate = string.Join(",", updateList);
                            break;
                        }
                    }
                }

                var middleIndex = (updateList.Count - 1) / 2;
                result += int.Parse(updateList[middleIndex]);
            }

            return result.ToString();
        }

        private void CreateRulesAndUpdates()
        {
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Contains('|'))
                    {
                        pageOrderingRules.Add(line);
                    }
                    else if (line.Contains(','))
                    {
                        updates.Add(line);
                    }
                }
            }
        }

        private bool CheckIfRightOrder(string update, bool flag = true)
        {
            var updateList = update.Split(',').ToList();

            foreach (var rule in pageOrderingRules)
            {
                var pagesRule = rule.Split('|');

                var firstIndex = updateList.FindIndex(x => x == pagesRule[0]);
                var secondIndex = updateList.FindIndex(x => x == pagesRule[1]);

                if ((updateList.Contains(pagesRule[0]) && updateList.Contains(pagesRule[1])) && firstIndex > secondIndex)
                {
                    if (flag == true)
                    {
                        updatesIncorrectOrder.Add(update);
                    }

                    return false;
                }
            }

            return true;
        }

        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}