using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    class Day2
    {
        public static int solveTask1()
        {
            int scoreSum = 0;
            Console.WriteLine(getWinner(PlayerAction.ROCK, PlayerAction.SCISSORS));
            Console.WriteLine((int)PlayerAction.ROCK == 1);
            foreach (string line in File.ReadLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task2Input.txt"))
            {
                PlayerAction p1 = getPlayerAction(line[0]);
                PlayerAction p2 = getPlayerAction(line[2]);
                switch (getWinner(p1, p2))
                {
                    case 1:
                        scoreSum += 0 + (int)p2;
                        break;
                    case 2:
                        scoreSum += 6 + (int)p2;
                        break;

                    default:
                        scoreSum += 3 + (int)p2;
                        break;
                }
            }
            return scoreSum;
        }

        public static int solveTask2()
        {
            int scoreSum = 0;

            foreach (string line in File.ReadLines(@"C:\Users\Kristen\Visual Studio Repos\Advent of Code\Advent of Code\Input\Task2Input.txt"))
            {
                PlayerAction p1 = getPlayerAction(line[0]);
                PlayerAction p2 = getPlayerAction(p1, line[2]);
                switch (getWinner(p1, p2))
                {
                    case 1:
                        scoreSum += 0 + (int)p2;
                        break;
                    case 2:
                        scoreSum += 6 + (int)p2;
                        break;
                    default:
                        scoreSum += 3 + (int)p2;
                        break;
                }
            }
            return scoreSum;
        }

        private static int getWinner(PlayerAction p1, PlayerAction p2)
        {
            //Returns 1 for player 1 win, 2 for player 2 win and 0 for draw
            if (((int)p1==3 && (int)p2 == 2) || ((int)p1 == 2 && (int)p2 == 1) || ((int)p1 == 1 && (int)p2 == 3))
            {
                return 1; // Player 1 win
            }
            else if (p1 == p2)
            {
                return 0; // Draw
            }
            return 2; // Player 2 win
        }

        private static PlayerAction getPlayerAction(char act)
        {
            switch (act)
            {
                case 'A':
                case 'X':
                    return PlayerAction.ROCK;
                case 'B':
                case 'Y':
                    return PlayerAction.PAPER;
                case 'C':
                case 'Z':
                    return PlayerAction.SCISSORS;
            }
            throw new ArgumentException();
        }
        private static PlayerAction getPlayerAction(PlayerAction player1Act, char gameState)
        {
            // Returns the appropriate shape given the shape of the first player and the desired game state (win, lose, draw)
            PlayerAction[] arr = new PlayerAction[] { PlayerAction.ROCK, PlayerAction.PAPER, PlayerAction.SCISSORS };
            switch (gameState)
            {
                // Take away from this exercise is definitely to use the modulo operator for this "wrap around" behaviour
                case 'X':
                    return arr[(((int)player1Act) - 2 + arr.Length) % arr.Length]; 
                case 'Y':
                    return arr[(((int)player1Act) - 1 + arr.Length) % arr.Length];
                case 'Z':
                    return arr[(((int)player1Act) + arr.Length) % arr.Length];
            }
            throw new ArgumentException();
        }
    }

    enum PlayerAction
    {
        ROCK = 1,
        PAPER = 2,
        SCISSORS = 3
    }
}
