using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                        increasing = decreasing = false; 
                        break; }
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
                        increasing = decreasing = false;
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

    }
}