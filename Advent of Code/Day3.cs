namespace Advent_of_Code
{
    internal class Day3
    {
        static Dictionary<char, int> charDict = getCharHashSet();
        private static Dictionary<char, int> getCharHashSet()
        {
            char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            Dictionary<char, int> charDict = new Dictionary<char, int>();
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                charDict.Add(c, i+1);
            }
            return charDict;
        }

        public static int solveTask1()
        {
            int misplacedItemsSum = 0;
            foreach (string line in File.ReadLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task3Input.txt"))
            {
                string compartment1 = line[0..(line.Length / 2)];
                string compartment2 = line[(line.Length/2)..line.Length];
                char misplacedItem = compartment1.Where(c => compartment2.Contains(c)).ElementAt(0);
                misplacedItemsSum += charDict[misplacedItem];
            }
            return misplacedItemsSum;
        }
        public static int solveTask2()
        {
            string[] elfGroup = new string[3];
            int badgeSum = 0;
            int i = 0;
            foreach (string line in File.ReadLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task3Input.txt"))
            {
                elfGroup[i] = line;
                i++;
                if (i > 2)
                {
                    char badge = elfGroup[0].Where(c => elfGroup[1].Contains(c) && elfGroup[2].Contains(c)).ElementAt(0);
                    badgeSum += charDict[badge];
                    i = 0;
                }
            }
            return badgeSum;
        }
    }
}
