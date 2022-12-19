using Advent_of_Code.Utility_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    internal class Day11
    {
        public static long solveTask1()
        {
            //string input = "Monkey 0:\r\n  Starting items: 79, 98\r\n  Operation: new = old * 19\r\n  Test: divisible by 23\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 3\r\n\r\nMonkey 1:\r\n  Starting items: 54, 65, 75, 74\r\n  Operation: new = old + 6\r\n  Test: divisible by 19\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 0\r\n\r\nMonkey 2:\r\n  Starting items: 79, 60, 97\r\n  Operation: new = old * old\r\n  Test: divisible by 13\r\n    If true: throw to monkey 1\r\n    If false: throw to monkey 3\r\n\r\nMonkey 3:\r\n  Starting items: 74\r\n  Operation: new = old + 3\r\n  Test: divisible by 17\r\n    If true: throw to monkey 0\r\n    If false: throw to monkey 1";
            string input = File.ReadAllText(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task11Input.txt");
            List<Monkey> monkeyList = parseInput(input);
            int roundCounter = 0;
            while (roundCounter < 20)
            {
                foreach (Monkey monkey in monkeyList)
                {
                    //Console.WriteLine($"Monkey {monkeyList.IndexOf(monkey)}; Monkey has {monkey.items.Count} items");
                    while (monkey.items.Count != 0)
                    {
                        long item = monkey.items.First.Value;
                        monkey.items.RemoveFirst();
                        //Inspecting item
                        item = monkey.applyOperation(item);
                        item = item / 3;
                        if (item % monkey.testDivisible == 0)
                        {
                            monkeyList[monkey.trueMonkey].items.AddLast(item);
                        }
                        else
                        {
                            monkeyList[monkey.falseMonkey].items.AddLast(item);
                        }
                        monkey.inspectCount++;

                    }

                }
                roundCounter++;
            }
            Console.WriteLine("----");
            foreach (Monkey monkey in monkeyList)
            {
                Console.WriteLine(monkey.ToString()+"\n");
            }
            List<long> itemTossAmount = monkeyList.Select(monkey => monkey.inspectCount).ToList();
            itemTossAmount.Sort();
            long monkeyBusiness = itemTossAmount[itemTossAmount.Count - 1] * itemTossAmount[itemTossAmount.Count - 2];
            return monkeyBusiness;
        }

        public static long solveTask2()
        {
            //string input = "Monkey 0:\r\n  Starting items: 79, 98\r\n  Operation: new = old * 19\r\n  Test: divisible by 23\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 3\r\n\r\nMonkey 1:\r\n  Starting items: 54, 65, 75, 74\r\n  Operation: new = old + 6\r\n  Test: divisible by 19\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 0\r\n\r\nMonkey 2:\r\n  Starting items: 79, 60, 97\r\n  Operation: new = old * old\r\n  Test: divisible by 13\r\n    If true: throw to monkey 1\r\n    If false: throw to monkey 3\r\n\r\nMonkey 3:\r\n  Starting items: 74\r\n  Operation: new = old + 3\r\n  Test: divisible by 17\r\n    If true: throw to monkey 0\r\n    If false: throw to monkey 1";
            string input = File.ReadAllText(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task11Input.txt");
            List<Monkey> monkeyList = parseInput(input);
            long masterDivisor = monkeyList.Select((monkey) => monkey.testDivisible).Aggregate((divisor, acc) => divisor*acc);
            int roundCounter = 0;
            while (roundCounter < 10000)
            {
                foreach (Monkey monkey in monkeyList)
                {
                    //Console.WriteLine($"Monkey {monkeyList.IndexOf(monkey)}; Monkey has {monkey.items.Count} items");
                    while (monkey.items.Count != 0)
                    {
                        long item = monkey.items.First.Value;
                        monkey.items.RemoveFirst();
                        item %= masterDivisor;
                        item = monkey.applyOperation(item);
                        if (item % monkey.testDivisible == 0)
                        {
                            monkeyList[monkey.trueMonkey].items.AddLast(item);
                        }
                        else
                        {
                            monkeyList[monkey.falseMonkey].items.AddLast(item);
                        }
                        monkey.inspectCount++;
                    }

                }
                roundCounter++;
            }
            List<long> itemTossAmount = monkeyList.Select(monkey => (long)monkey.inspectCount).ToList();
            Console.WriteLine(string.Join(",", itemTossAmount));
            itemTossAmount.Sort();
            long monkeyBusiness = itemTossAmount[itemTossAmount.Count - 1] * itemTossAmount[itemTossAmount.Count - 2];
            return monkeyBusiness;
        }


        private static List<Monkey> parseInput(string input)
        {
            List<Monkey> monkeys = new List<Monkey>();
            string[] monkeyStrings = input.Split("\r\n\r\n");
            foreach (string monkeyString in monkeyStrings)
            {
                monkeys.Add(Monkey.parseMonkey(monkeyString));
            }
            return monkeys;
        }
    }
}
