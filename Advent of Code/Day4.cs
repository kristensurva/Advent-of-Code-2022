using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    internal class Day4
    {
        public static int solveTask1()
        {
            int sum = 0;
            foreach (string line in File.ReadLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task4Input.txt"))
            {
                string[] pairs = line.Split(',');
                int firstPairStartingValue = int.Parse(pairs[0].Split('-')[0]);
                int firstPairEndingValue = int.Parse(pairs[0].Split('-')[1]);
                int secondPairStartingValue = int.Parse(pairs[1].Split('-')[0]);
                int secondPairEndingValue = int.Parse(pairs[1].Split('-')[1]);

                if ((firstPairStartingValue<=secondPairStartingValue && firstPairEndingValue>=secondPairEndingValue) 
                    || (firstPairStartingValue >= secondPairStartingValue && firstPairEndingValue <= secondPairEndingValue))
                {
                    sum++;
                }
            }
            return sum;
        }

        public static int solveTask2()
        {
            int sum = 0;
            foreach (string line in File.ReadLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task4Input.txt"))
            {
                string[] pairs = line.Split(',');
                int firstPairStartingValue = int.Parse(pairs[0].Split('-')[0]);
                int firstPairEndingValue = int.Parse(pairs[0].Split('-')[1]);
                int secondPairStartingValue = int.Parse(pairs[1].Split('-')[0]);
                int secondPairEndingValue = int.Parse(pairs[1].Split('-')[1]);
                if ((firstPairEndingValue >= secondPairStartingValue && firstPairStartingValue<=secondPairStartingValue) 
                    || (secondPairEndingValue >= firstPairStartingValue && secondPairStartingValue <= firstPairStartingValue))
                {
                    sum++;
                }
            }
            return sum;
        }
    }
}
