using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    internal class Day8
    {
        public static int solveTask1()
        {
            int[,] forest = parseForest();
            int amountOfVisibleTrees = 0;
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    if (isTreeVisible(forest, i, j))
                    {
                        amountOfVisibleTrees++;
                    }
                }
            }
            return amountOfVisibleTrees;
        }

        public static int solveTask2()
        {
            int[,] forest = parseForest();
            int maxScenicValue = 0;
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    int temp = scenicValue(forest, i, j);
                    if (temp > maxScenicValue)
                    {
                        maxScenicValue = temp;
                    }
                }
            }
            return maxScenicValue;
        }

        private static int[,] parseForest()
        {
            //string[] input = "30373\r\n25512\r\n65332\r\n33549\r\n35390".Split("\r\n");
            string[] input = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task8Input.txt");
            int width = input[0].Length;
            int height = input.Length;
            int[,] forest = new int[height, width];
            int i = 0;
            foreach (string line in input)
            {
                int j = 0;
                foreach (char treeHeight in line.ToCharArray())
                {
                    forest[i,j] = int.Parse(treeHeight.ToString());
                    j++;
                }
                i++;
            }
            return forest;
        }

        private static bool isTreeVisible(int[,] forest, int row, int col)
        {
            int treeHeight = forest[row, col];
            bool isVisible = true;
            // Visible from West
            for (int i = 0; i < row; i++)
            {
                if (forest[i, col]>=treeHeight)
                {
                    isVisible = false;
                    break;
                }
            }
            if (isVisible) return true;
            isVisible = true;
            // Visible from East
            for (int i = row+1; i < forest.GetLength(0); i++)
            {
                if (forest[i, col] >= treeHeight)
                {
                    isVisible = false;
                    break;
                }
            }
            if (isVisible) return true;
            isVisible = true;
            // Visible from North
            for (int i = 0; i < col; i++)
            {
                if (forest[row, i] >= treeHeight)
                {
                    isVisible = false;
                    break;
                }
            }
            if (isVisible) return true;
            isVisible = true;
            // Visible from South
            for (int i = col+1; i < forest.GetLength(1); i++)
            {
                if (forest[row, i] >= treeHeight)
                {
                    isVisible = false;
                    break;
                }
            }
            if (isVisible) return true;
            return isVisible;
        }

        private static int scenicValue(int[,] forest, int row, int col)
        {
            if (row == 0 || col == 0 || row == forest.GetLength(0) - 1 || col == forest.GetLength(1) - 1) return 0; // On the border
            int northVis, southVis, westVis, eastVis; // Visibility
            int treeHeight = forest[row, col];
            // North visibility
            northVis = 1;
            for (int i = row-1; i >= 0; i--)
            {
                if (forest[i, col]>=treeHeight || i==0)
                {
                    break;
                }
                northVis++;
            }
            // South visibility
            southVis = 1;
            for (int i = row+1; i < forest.GetLength(0); i++)
            {
                if (forest[i, col] >= treeHeight || i== forest.GetLength(0)-1)
                {
                    break;
                }
                southVis++;
            }
            // West visibility
            westVis = 1;
            for (int i = col-1; i >= 0; i--)
            {
                if (forest[row, i] >= treeHeight || i==0)
                {
                    break;
                }
                westVis++;
            }
            // East visibility
            eastVis = 1;
            for (int i = col+1; i < forest.GetLength(1); i++)
            {
                if (forest[row, i] >= treeHeight || i == forest.GetLength(1)-1)
                {
                    break;
                }
                eastVis++;
            }
            return northVis * southVis * eastVis * westVis;
        }
    }
}
