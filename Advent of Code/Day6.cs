using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    internal class Day6
    {
        public static int solveTask1()
        {
            string input = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task6Input.txt")[0];
            for (int i = 0; i < input.Length; i++)
            {
                string packet = input.Substring(i, 4);
                if (isStartOfPacketMarker(packet))
                {
                    return i+4;
                }
            }
            return -1;
        }

        public static int solveTask2()
        {
            string input = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task6Input.txt")[0];
            for (int i = 0; i < input.Length; i++)
            {
                string packet = input.Substring(i, 14);
                if (isStartOfPacketMarker(packet))
                {
                    return i + 14;
                }
            }
            return -1;
        }

        public static void solveTask2Anna()
        {
            string ctrl = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task6Input.txt")[0];
            List<char> radio = new List<char>();
            for (int i = 0; i < ctrl.Length; i++)
            {
                if (radio.Contains(ctrl[i]))
                {
                    int delet = radio.IndexOf((ctrl[i]));
                    radio = radio.GetRange(delet + 1, radio.Count-(delet+1));
                }
                radio.Add(ctrl[i]);
                if (radio.Count > 13)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }

        private static bool isStartOfPacketMarker(string packet)
        {
            for (int j = 0; j < packet.Length; j++)
            {
                if (packet.LastIndexOf(packet[j]) != j)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
