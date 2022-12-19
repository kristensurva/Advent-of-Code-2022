using Advent_of_Code.Utility_Classes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace Advent_of_Code
{
    internal class Day12
    {
        static (int row, int col) startingCoords;
        static (int row, int col) endingCoords;

        public static int solveTask1()
        {
            
            //string[] input = "Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi".Split("\r\n");
            //string[] input = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task12Test2.txt");
            string[] input = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task12Input.txt");
            MapNode.map = parseMap(input);
            printMap(MapNode.map);
            MapNode destinationNode = pathFind(startingCoords, endingCoords);
            printMap(MapNode.map, destinationNode);
            renderMap(MapNode.map, destinationNode);
            return destinationNode.path.Count-1;
        }

        public static int solveTask2()
        {
            string[] input = File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task12Input.txt");
            MapNode.map = parseMap(input);
            List<(int, int)> startingCoordinatesList = getAllCoordinatesOfHeight('a', MapNode.map);
            List<MapNode> allPathsToFinish = new List<MapNode>();
            foreach ((int, int) startingCoordinates in startingCoordinatesList)
            {
                MapNode temp = pathFind(startingCoordinates, endingCoords);
                if (temp != null)
                {
                    allPathsToFinish.Add(temp);
                }
            }
            MapNode shortestPath = allPathsToFinish.OrderBy(x => x.path.Count).ToArray()[0];
            printMap(MapNode.map, shortestPath);
            return shortestPath.path.Count-1;
        }

        private static List<(int, int)> getAllCoordinatesOfHeight(char c, char[,] map)
        {
            List<(int, int)> coordinates = new List<(int, int)>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j]==c)
                    {
                        coordinates.Add((i,j));
                    }
                }
            }
            return coordinates;
        }

        private static char[,] parseMap(string[] input)
        {
            char[,] map = new char[input.Length, input[0].Length];
            int i = 0;
            foreach (string line in input)
            {
                int j = 0;
                foreach(char c in line)
                {
                    if (c == 'S') {
                        startingCoords = (i, j);
                        map[i, j] = 'a';
                    }
                    else if (c == 'E')
                    {
                        endingCoords = (i, j);
                        map[i, j] = 'z';
                    }
                    else
                    {
                        map[i, j] = c;
                    }
                    j++;
                }
                i++;
            }
            return map;
        }

        private static MapNode pathFind((int, int) startingCoordinates, (int,int) targetCoordinates)
        {
            Dictionary<(int row, int col), MapNode> exploredNodes = new Dictionary<(int row, int col), MapNode>();
            List<MapNode> toBeExploredNodes = new List<MapNode>();
            toBeExploredNodes.Add(new MapNode(startingCoordinates, new HashSet<(int, int)>(), targetCoordinates));
            // Implementation of A*, where the heuristic is calculated on the 3 dimensional distance of node from target node + current path length
            while (toBeExploredNodes.Count != 0)
            {
                toBeExploredNodes = toBeExploredNodes.OrderBy(x => x.distance).ToList();
                //Console.WriteLine($"Not Explored: [{string.Join(",", toBeExploredNodes.Select(x => x.coordinates))}]");
                MapNode currentNode = toBeExploredNodes[0];
                toBeExploredNodes.RemoveAt(0);
                currentNode.path.Add(currentNode.coordinates);

                void placeIntoNodeQueue((int row, int col) tempCoordinates)
                {
                    // Predicate checks if coordinates exit bounds
                    if (tempCoordinates.row < 0 || tempCoordinates.row >= MapNode.map.GetLength(0) || tempCoordinates.col < 0 || tempCoordinates.col >= MapNode.map.GetLength(1))
                    {
                        return;
                    }
                    // First predicate checks if that node is already explored, Second predicate checks if its climbable
                    if (!exploredNodes.Keys.Contains(tempCoordinates) && currentNode.height - MapNode.map[tempCoordinates.row, tempCoordinates.col] >= -1)
                    {
                        MapNode newNode = new MapNode(tempCoordinates, new HashSet<(int, int)>(currentNode.path), targetCoordinates);
                        if (toBeExploredNodes.Any(x => x.coordinates == tempCoordinates))
                        {
                            MapNode existingNode = toBeExploredNodes.Where(x => x.coordinates == tempCoordinates).ToArray()[0];
                            if (newNode.distance < existingNode.distance)
                            {
                                toBeExploredNodes.Remove(existingNode);
                                toBeExploredNodes.Add(newNode);
                            }
                        }
                        else
                        {
                            toBeExploredNodes.Add(newNode);
                        }
                    }
                }
                // North
                (int row, int col) tempCoordinates = currentNode.coordinates;
                tempCoordinates.row--;
                placeIntoNodeQueue(tempCoordinates);
                // South
                tempCoordinates = currentNode.coordinates;
                tempCoordinates.row++;
                placeIntoNodeQueue(tempCoordinates);
                // West
                tempCoordinates = currentNode.coordinates;
                tempCoordinates.col--;
                placeIntoNodeQueue(tempCoordinates);
                // East
                tempCoordinates = currentNode.coordinates;
                tempCoordinates.col++;
                placeIntoNodeQueue(tempCoordinates);
                exploredNodes.Add(currentNode.coordinates, currentNode);
                //printMap(MapNode.map, currentNode);
            }
            if (exploredNodes.ContainsKey(targetCoordinates))
            {
                return exploredNodes[targetCoordinates];
            }
            else
            {
                return null;
            }
        }

        private static void printMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void printMap(char[,] map, MapNode destinationNode)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (destinationNode.path.Contains((i, j)))
                    {
                        Console.Write('#');
                    }
                    else if ((i, j) == startingCoords)
                    {
                        Console.Write('S');
                    }
                    else if ((i, j) == endingCoords)
                    {
                        Console.Write('E');
                    }
                    else
                    {
                        Console.Write(map[i, j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void renderMap(char[,] map)
        {
            Bitmap pic = new Bitmap(map.GetLength(0), map.GetLength(1));
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int color = (int)((map[i, j] - 'a') / 26f * 255f);
                    pic.SetPixel(i, j, Color.FromArgb(255, color, color, color));
                }
            }
            pic.Save(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\map.png");
        }

        private static void renderMap(char[,] map, MapNode destinationNode)
        {
            Bitmap pic = new Bitmap(map.GetLength(0), map.GetLength(1));
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (destinationNode.path.Contains((i, j)))
                    {
                        pic.SetPixel(i, j, Color.FromArgb(255, 240, 10, 10));
                    }
                    else
                    {
                        int color = (int)((map[i, j] - 'a') / 26f * 255f);
                        pic.SetPixel(i, j, Color.FromArgb(255, color, color, color));
                    }
                }
            }
            pic.Save(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\map.png");
        }
    }
}