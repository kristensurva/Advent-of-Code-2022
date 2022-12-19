using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    // Day 7 class
    internal class Node
    {
        public static int fileSystemMaxCapacity = 70000000;
        public static int capacityNeeded = 30000000;

        public List<int> leaves; // Actual files (or rather the sizes of these files)
        public Dictionary<string, Node> nodes;
        public Node parentDir;
        private int dirSize = -1; // Cache value 

        public Node(Node parentDir)
        {
            this.leaves = new List<int>();
            this.nodes = new Dictionary<string, Node>();
            this.parentDir = parentDir;
        }

        public Node()
        {
            this.leaves = new List<int>();
            this.nodes = new Dictionary<string, Node>();
            this.parentDir = null;
        }

        public int sumFileSizes()
        {
            if (dirSize!=-1) // If size already calculated
            {
                return dirSize;
            }
            int sum = 0;
            foreach (Node node in nodes.Values)
            {
                sum += node.sumFileSizes();
            }
            foreach (int leaf in leaves)
            {
                sum += leaf;
            }
            return sum;
        }

        public int sumOfFilesSmallerThanMax(int max)
        {
            int amount = 0;
            foreach(Node node in nodes.Values)
            {
                amount += node.sumOfFilesSmallerThanMax(max);
            }
            if (sumFileSizes() <= max)
            {
                amount+= sumFileSizes();
            }
            return amount;
        }

        public List<int> filesBigEnough(int min)
        {
            List<int> files = new List<int>();
            foreach (Node node in nodes.Values)
            {
                files.AddRange(node.filesBigEnough(min));
            }
            if (sumFileSizes() >= min)
            {
                files.Add(sumFileSizes());
            }
            return files;
        }
    }
}
