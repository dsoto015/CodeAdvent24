using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CodeAdvent24
{
    [TestClass]
    public class AdventCode
    {
        [TestMethod]
        public void DayOne()
        {
            var input = PuzzleInputs.DayOne;
            var listOfPairs = input.Split("\r\n");
            var leftList = new List<int>();
            var rightList = new List<int>();
            foreach (var pair in listOfPairs)
            {
                var newPair = pair.Split("  ");
                leftList.Add(int.Parse(newPair[0]));
                rightList.Add(int.Parse(newPair[1]));
            }

            var orderedLeftList = leftList.OrderBy(x => x).ToList();
            var orderedRightList = rightList.OrderBy(x => x).ToList();

            var total = 0;
            for(int i = 0; i < orderedLeftList.Count; i++)
            {
                total += Math.Abs(orderedLeftList[i] - orderedRightList[i]);
            }

            Assert.AreEqual(2196996, total);
        }

        [TestMethod]
        public void DayTwoPartOne()
        {
            var input = PuzzleInputs.DayTwo;
            var reports = input.Split("\r\n");
            var safeCounter = 0;

            for (int i = 0; i < reports.Length; i++)
            {
                var reportArray = reports[i].Split(' ').Select(int.Parse).ToArray();

                bool increasing = true;
                bool decreasing = true;
                for (int j = 1; j < reportArray.Length; j++)
                {
                    if (reportArray[j] > reportArray[j - 1]) 
                        decreasing = false;
                    if (reportArray[j] < reportArray[j - 1]) 
                        increasing = false;

                    if (reportArray[j] == reportArray[j - 1]) 
                    {
                        increasing = false; 
                        decreasing = false; 
                        break; 
                    }
                }
                bool isMonotonic = increasing || decreasing;

                if (isMonotonic)
                {
                    bool isWithinBounds = true;
                    for (int j = 1; j < reportArray.Length; j++)
                    {
                        int diff = Math.Abs(reportArray[j] - reportArray[j - 1]);
                        if (diff < 1 || diff > 3)
                        {
                            isWithinBounds = false;
                            break;
                        }
                    }

                    if (isWithinBounds) 
                        safeCounter++;
                }
            }

            Console.WriteLine($"Safe Reports = {safeCounter}");
            Assert.AreEqual(334, safeCounter);
        }

        [TestMethod]
        public void DayTwoPartTwo()
        {
            var input = PuzzleInputs.DayTwo;
            var reports = input.Split("\r\n");
            var safeCounter = 0;

            for (int i = 0; i < reports.Length; i++)
            {
                var reportArray = reports[i].Split(' ').Select(int.Parse).ToArray();

                if (IsSafe(reportArray))
                {
                    safeCounter++;
                    continue;
                }

                bool safeWithDampener = false;
                for (int j = 0; j < reportArray.Length; j++)
                {
                    var modifiedReport = reportArray.Where((_, x) => x != j).ToArray();
                    if (IsSafe(modifiedReport))
                    {
                        safeWithDampener = true;
                        break;
                    }
                }

                if (safeWithDampener)
                    safeCounter++;
            }

            bool IsSafe(int[] report)
            {
                if (report.Length < 2) return false;

                bool increasing = true;
                bool decreasing = true;
                for (int j = 1; j < report.Length; j++)
                {
                    if (report[j] > report[j - 1])
                        decreasing = false;
                    if (report[j] < report[j - 1]) 
                        increasing = false;

                    if (report[j] == report[j - 1])
                    {
                        increasing = false;
                        decreasing = false;
                        break;
                    }
                }
                bool isMonotonic = increasing || decreasing;

                if (!isMonotonic)
                    return false;

                for (int j = 1; j < report.Length; j++)
                {
                    int diff = Math.Abs(report[j] - report[j - 1]);
                    if (diff < 1 || diff > 3) 
                        return false;
                }

                return true;
            }

            Console.WriteLine($"Safe Reports with Dampener = {safeCounter}");
            Assert.AreEqual(400, safeCounter);
        }

        [TestMethod]
        public void DayThreePartOne()
        {
            var input = PuzzleInputs.DayThree;
            var pattern = @"mul\(\d+,\d+\)";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var numbersToAdd = new List<int>();
            var matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                if(match.Success)
                {
                    var mulMatch = match.Value;
                    var factors = mulMatch.Split('(', ')')[1].Split(',').Select(x => int.Parse(x)).ToList();
                    var product = factors[0] * factors[1];
                    numbersToAdd.Add(product);
                }
            }
            var sum = numbersToAdd.Sum();
            Assert.AreEqual(188741603, sum);
        }

        [TestMethod]
        public void DayThreePartTwo()
        {
            var input = PuzzleInputs.DayThree;

            var mulPattern = @"mul\(\d+,\d+\)";
            var dontPattern = @"don't\(\)";
            var doPattern = @"do\(\)";

            var regex = new Regex($"{mulPattern}|{dontPattern}|{doPattern}", RegexOptions.IgnoreCase);
            var matches = regex.Matches(input);
           
            var cleanedInput = new List<string>();
            var shouldKeep = true;
            var dontPhrase = "don't()";
            var doPhrase = "do()";

            var numbersToAdd = new List<int>();
            foreach (Match match in matches)
            {
                var phrase = match.Value;
                if(phrase == doPhrase)
                {
                    shouldKeep = true;
                }

                if(phrase == dontPhrase)
                {
                    shouldKeep = false;
                    continue;
                }

                if (shouldKeep && phrase != doPhrase)
                {
                    var factors = phrase.Split('(',')')[1].Split(',').Select(x => int.Parse(x)).ToArray();
                    var product = factors[0] * factors[1];
                    numbersToAdd.Add(product);
                }
            }

            var sum = numbersToAdd.Sum();
            Assert.AreEqual(67269798, sum);
        }

        [TestMethod]
        public void DayFourDayOne()
        {
            var input = PuzzleInputs.DayFour;
            var wordSearchArr = input.Split("\r\n");
            var twoDimensionalArr = new string[wordSearchArr.Length, wordSearchArr[0].Length];

            for (int i = 0; i < wordSearchArr.Length; i++)
            {
                for (int j = 0; j < wordSearchArr[i].Length; j++)
                {
                    twoDimensionalArr[i, j] = wordSearchArr[i][j].ToString();
                }
            }

            var totalXmasCount = 0;
            for (int i = 0; i < wordSearchArr.Length; i++)
            {
                for (int j = 0; j < wordSearchArr[i].Length; j++)
                {
                    var xmasCount = GetXmasCountFromReference(i, j, twoDimensionalArr[i, j]);
                    totalXmasCount += xmasCount;
                }
            }

            Console.WriteLine($"total xmas count = {totalXmasCount}");


            int GetXmasCountFromReference(int xCoord, int yCoord, string letter)
            {
                //8 possible directions
                var directions = new (int dx, int dy)[]
                {
                    (0, 1),
                    (1, 1),
                    (1, 0),
                    (1, -1),
                    (0, -1),
                    (-1, -1),
                    (-1, 0),
                    (-1, 1)
                };

                var referenceLetter = letter.ToUpper();
                var eightPossible = new List<string>();

                foreach (var (dx, dy) in directions)
                {
                    var letters = new List<string> { referenceLetter };

                    for (int step = 1; step <= 3; step++)
                    {
                        var coord = (x: xCoord + dx * step, y: yCoord + dy * step);
                        letters.Add(GetLetter(coord));
                    }

                    eightPossible.Add(String.Concat(letters.Where(l => !string.IsNullOrEmpty(l)).OrderBy(c => "XMAS".IndexOf(c))));
                }

                return eightPossible.Count(word => word == "XMAS");

                string GetLetter((int x, int y) coord)
                {
                    if (coord.x < 0 || coord.x >= twoDimensionalArr.GetLength(0) ||
                        coord.y < 0 || coord.y >= twoDimensionalArr.GetLength(1))
                    {
                        return string.Empty;
                    }

                    return twoDimensionalArr[coord.x, coord.y].ToUpper();
                }
            }




        }


    }
}