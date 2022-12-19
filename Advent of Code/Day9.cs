using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    internal class Day9
    {
        public static int solveTask1()
        {
            // 1st element is x coordinate, 2nd element is y coordinate
            (int x, int y) headPosition = (0, 0);
            (int x, int y) tailPosition = (0, 0);
            HashSet<(int, int)> tailVisitedPositions = new HashSet<(int, int)>();
            //string[] input = "R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2".Split("\r\n");
            string[] input = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task9Input.txt");
            foreach (string line in input)
            {
                string[] tokens = line.Split(" ");
                switch (tokens[0])
                {
                    case "R":
                        for (int i = 0; i < int.Parse(tokens[1]); i++)
                        {
                            headPosition.x++;
                            tailPosition = moveTail(tailPosition, headPosition);
                            tailVisitedPositions.Add(tailPosition);
                        }
                        break;
                    case "L":
                        for (int i = 0; i < int.Parse(tokens[1]); i++)
                        {
                            headPosition.x--;
                            tailPosition = moveTail(tailPosition, headPosition);
                            tailVisitedPositions.Add(tailPosition);
                        }
                        break;
                    case "U":
                        for (int i = 0; i < int.Parse(tokens[1]); i++)
                        {
                            headPosition.y++;
                            tailPosition = moveTail(tailPosition, headPosition);
                            tailVisitedPositions.Add(tailPosition);
                        }
                        break;
                    case "D":
                        for (int i = 0; i < int.Parse(tokens[1]); i++)
                        {
                            headPosition.y--;
                            tailPosition = moveTail(tailPosition, headPosition);
                            tailVisitedPositions.Add(tailPosition);
                        }
                        break;
                }
            }
            return tailVisitedPositions.Count;
        }

        public static int solveTask2()
        {
            // 1st element is x coordinate, 2nd element is y coordinate
            (int x, int y)[] rope = new (int,int)[10];
            HashSet<(int, int)> tailVisitedPositions = new HashSet<(int, int)>();
            //string[] input = "R 5\r\nU 8\r\nL 8\r\nD 3\r\nR 17\r\nD 10\r\nL 25\r\nU 20".Split("\r\n");
            string[] input = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task9Input.txt");
            foreach (string line in input)
            {
                string[] tokens = line.Split(" ");
                switch (tokens[0])
                {
                    case "R":
                        for (int i = 0; i < int.Parse(tokens[1]); i++)
                        {
                            rope[0].x++;
                            for (int j = 1; j < rope.Length; j++)
                            {
                                rope[j] = moveTail(rope[j], rope[j-1]);
                                
                            }
                            tailVisitedPositions.Add(rope[rope.Length - 1]);
                        }
                        break;
                    case "L":
                        for (int i = 0; i < int.Parse(tokens[1]); i++)
                        {
                            rope[0].x--;
                            for (int j = 1; j < rope.Length; j++)
                            {
                                rope[j] = moveTail(rope[j], rope[j - 1]);

                            }
                            tailVisitedPositions.Add(rope[rope.Length - 1]);
                        }
                        break;
                    case "U":
                        for (int i = 0; i < int.Parse(tokens[1]); i++)
                        {
                            rope[0].y++;
                            for (int j = 1; j < rope.Length; j++)
                            {
                                rope[j] = moveTail(rope[j], rope[j - 1]);

                            }
                            tailVisitedPositions.Add(rope[rope.Length - 1]);
                        }
                        break;
                    case "D":
                        for (int i = 0; i < int.Parse(tokens[1]); i++)
                        {
                            rope[0].y--;
                            for (int j = 1; j < rope.Length; j++)
                            {
                                rope[j] = moveTail(rope[j], rope[j - 1]);

                            }
                            tailVisitedPositions.Add(rope[rope.Length-1]);
                        }
                        break;
                }
            }
            return tailVisitedPositions.Count;
        }


        private static (int,int) moveTail((int x, int y) tailPosition, (int x, int y) headPosition)
        {
            // If tail and head are touching, we don't move tail
            if (Math.Abs(headPosition.x-tailPosition.x)<=1 && Math.Abs(headPosition.y - tailPosition.y) <= 1)
            {
                return tailPosition;
            }
            // Move tail
            if (headPosition.x-tailPosition.x!=0)
            {
                int xDirectionToMove = (headPosition.x - tailPosition.x) / Math.Abs(headPosition.x - tailPosition.x);
                tailPosition.x += xDirectionToMove;
            }
            if (headPosition.y - tailPosition.y != 0) { 
                int yDirectionToMove = (headPosition.y - tailPosition.y) / Math.Abs(headPosition.y - tailPosition.y);
                tailPosition.y += yDirectionToMove;
            }
            return tailPosition;
        }
    }
}
