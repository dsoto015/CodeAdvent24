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
    }
}