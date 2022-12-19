using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    internal class Day7
    {
        public static int solveTask1()
        {
            Node fileTree = readInFileTree();
            return fileTree.sumOfFilesSmallerThanMax(100000);
        }

        public static int solveTask2()
        {
            Node fileTree = readInFileTree();
            int fileTreeSize = fileTree.sumFileSizes();
            int unUsedSpace = Node.fileSystemMaxCapacity - fileTreeSize;
            int spaceNeededToUpdate = Node.capacityNeeded - unUsedSpace;
            List<int> directories = fileTree.filesBigEnough(spaceNeededToUpdate);
            return directories.Min();
        }

        private static Node readInFileTree()
        {
            Node currentDir = new Node();
            Node motherOfAllNodes = currentDir;
            currentDir.nodes.Add("/", new Node(currentDir));
            foreach (string line in File.ReadAllLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task7Input.txt"))
            {
                string[] tokens = line.Split(' ');
                if (line.StartsWith('$')) // Is a command
                {
                    switch (tokens[1])
                    {
                        case "cd":
                            if (tokens[2] == "..")
                            {
                                currentDir = currentDir.parentDir;
                            }
                            else
                            {
                                currentDir = currentDir.nodes[tokens[2]];
                            }
                            break;
                        case "ls":
                            break;
                    }
                }
                else
                {
                    if (tokens[0] == "dir")
                    {
                        currentDir.nodes.Add(tokens[1], new Node(currentDir));
                    }
                    else
                    {
                        currentDir.leaves.Add(int.Parse(tokens[0]));
                    }
                }
            }
            return motherOfAllNodes;
        }
    }
}
