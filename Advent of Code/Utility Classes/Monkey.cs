using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Advent_of_Code.Utility_Classes
{
    internal class Monkey
    {
        public LinkedList<long> items;
        public string operation;
        public long testDivisible;
        public int trueMonkey;
        public int falseMonkey;
        public long inspectCount = 0;

        public Monkey(LinkedList<long> items, string operation, long testDivisible, int trueMonkey, int falseMonkey)
        {
            this.items = items;
            this.operation = operation;
            this.testDivisible = testDivisible;
            this.trueMonkey = trueMonkey;
            this.falseMonkey = falseMonkey;
        }

        public static Monkey parseMonkey(string monkeyString)
        {
            string[] monkeyAttributes = monkeyString.Split("\r\n");
            LinkedList<long> items = new LinkedList<long>(monkeyAttributes[1].Split(": ")[1].Split(", ").Select((s) => long.Parse(s)));
            string operation = monkeyAttributes[2].Split(" = ")[1];
            long testDivisible = long.Parse(monkeyAttributes[3].Split("by ")[1]);
            int trueMonkey = int.Parse(monkeyAttributes[4].Split("monkey ")[1]);
            int falseMonkey = int.Parse(monkeyAttributes[5].Split("monkey ")[1]);
            Monkey monkey = new Monkey(items, operation, testDivisible, trueMonkey, falseMonkey);
            Console.WriteLine(monkey.ToString() + "\n");
            return monkey;
        }

        public long applyOperation(long old)
        {
            string tempOperation = operation;
            tempOperation = tempOperation.Replace("old", old.ToString()); // replace "old" with number
            //Console.WriteLine(tempOperation);
            if (tempOperation.Contains('+')) {
                long number = long.Parse(tempOperation.Split(" + ")[1]);
                return old + number;
            }
            else if (tempOperation.Contains('*'))
            {
                long number = long.Parse(tempOperation.Split(" * ")[1]);
                return old * number;
            }
            return -1;
        }

        public override string ToString()
        {
            return $"[{string.Join(",", items)}]\n{operation}\n{testDivisible}\n{trueMonkey}\n{falseMonkey}";
        }


    }
}
