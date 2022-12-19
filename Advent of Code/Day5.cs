using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Advent_of_Code
{
    internal class Day5
    {
        public static String solveTask1() 
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task5Input.txt");
            Stack<char>[] stacks = parseStacks(lines);
            Regex regex = new Regex(@"\d+"); // Get digit
            foreach (string line in lines.Where(c => c.StartsWith("move")))
            {
                Match[] matches = regex.Matches(line).ToArray();
                int amountToMove = int.Parse(matches[0].Value);
                int from = int.Parse(matches[1].Value)-1;
                int to = int.Parse(matches[2].Value)-1;
                for (int i = 0; i < amountToMove; i++)
                {
                    char hand = stacks[from].Pop();
                    stacks[to].Push(hand);
                }
            }
            return String.Join("", stacks.Select(stack => stack.Pop()));
        }

        public static String solveTask2()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task5Input.txt");
            Stack<char>[] stacks = parseStacks(lines);
            Regex regex = new Regex(@"\d+"); // Get digit
            foreach (string line in lines.Where(c => c.StartsWith("move")))
            {
                Match[] matches = regex.Matches(line).ToArray();
                int amountToMove = int.Parse(matches[0].Value);
                int from = int.Parse(matches[1].Value)-1;
                int to = int.Parse(matches[2].Value)-1;
                Stack<char> hand = new Stack<char>();
                for (; amountToMove > 0; amountToMove--)
                {
                    hand.Push(stacks[from].Pop());
                }
                while (hand.Count > 0)
                {
                    char c = hand.Pop();
                    stacks[to].Push(c);
                }
            }
            return String.Join("", stacks.Select(stack => stack.Pop()));
        }

        private static Stack<char>[] parseStacks(string[] lines)
        {
            Stack<char>[] stacks = null;
            foreach(string line in lines)
            {
                if (line.StartsWith(" 1 ")) break;
                string lineFixed = new string(line.Substring(1, line.Length - 2).Where((c, i) => i % 4 == 0).ToArray());
                // Initialising stacks
                if (stacks == null)
                {
                    stacks = new Stack<char>[lineFixed.Length];
                    for (int i = 0; i < lineFixed.Length; i++)
                    {
                        stacks[i] = new Stack<char>();
                    }
                }

                for (int i = 0; i < lineFixed.Length; i++)
                {
                    if (!lineFixed[i].Equals(' '))
                    {
                        stacks[i].Push(lineFixed[i]);
                    }
                }
            }
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack<char>(stacks[i]); // This reverses stack
            }
            return stacks;
        }
    }
}
