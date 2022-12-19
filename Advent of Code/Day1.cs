using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Runtime.InteropServices;

class Day1
{
    public static int solveTask1()
    {
        // Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
        List<int> elfCalories = new List<int>();
        int calorieSum = 0;
        Console.WriteLine(Directory.GetCurrentDirectory());
        foreach (string line in System.IO.File.ReadLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task1Input.txt"))
        {
            if (line=="")
            {
                elfCalories.Add(calorieSum);
                calorieSum = 0;
            }
            else
            {
                calorieSum += int.Parse(line);                
            }
        }
        return elfCalories.Max();
    }

    public static int solveTask2()
    {
        // Find the top three Elves carrying the most Calories. How many Calories are those Elves carrying in total?
        List<int> elfCalories = new List<int>();
        int calorieSum = 0;
        Console.WriteLine(Directory.GetCurrentDirectory());
        foreach (string line in System.IO.File.ReadLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task1Input.txt"))
        {
            if (line == "")
            {
                elfCalories.Add(calorieSum);
                calorieSum = 0;
            }
            else
            {
                calorieSum += int.Parse(line);
            }
        }
        elfCalories.Sort();
        //int finalSum = 0;
        ////Console.WriteLine(string.Join(",", elfCalories.ToArray()));
        //for (int i = elfCalories.Count-1; i > elfCalories.Count-4; i--)
        //{
        //    //Console.WriteLine(elfCalories[i]);
        //    finalSum += elfCalories[i];
        //}
        return elfCalories.GetRange(elfCalories.Count - 3, 3).Aggregate((acc, x) => acc + x); // So much shorter!
    }
}