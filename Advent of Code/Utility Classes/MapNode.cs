using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code.Utility_Classes
{
    internal class MapNode
    {
        public (int row, int col) coordinates;
        public HashSet<(int, int)> path;
        public int distance;
        public char height;
        public static char[,] map = null;

        public MapNode((int row, int col) coordinates, HashSet<(int, int)> path, (int,int) target)
        {
            this.coordinates = coordinates;
            this.path = path;
            this.height = map[coordinates.row, coordinates.col];
            distance = calculateDistance(target);
        }

        private int calculateDistance((int row, int col) targetCoordinates)
        {
            int xDistance = Math.Abs(targetCoordinates.row - coordinates.row);
            int yDistance = Math.Abs(targetCoordinates.col - coordinates.col);
            int zDistance = Math.Abs(map[targetCoordinates.row, targetCoordinates.col] - map[coordinates.row, coordinates.col]);
            return xDistance + yDistance + zDistance + path.Count;
        }
    }
}
