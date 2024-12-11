

using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;

namespace BattleShip.Models
{
    public class ShipBoard
    {
        private List<List<string>> shipBoard = new List<List<string>>(); 

        public void PlaceShip(List<string> location)
        {
            if (shipBoard == null)
            {
                ArrayList shipBoard = new ArrayList();
            }
            shipBoard.Add(location);
        }

        // check to see if ship is sunk
        public bool Sink()
        {
            bool result = false;

            for (int i = 0; i < shipBoard.Count; i++) 
            {
                for (int j = 0; j < shipBoard.ElementAt(i).Count(); j++) 
                {
                    if (j.Equals(null))
                    {
                        shipBoard.Remove(shipBoard.ElementAt(i));
                        result = true;
                        Console.ReadLine(); 
                        break;
                    }
                }
            }
            return result;
        }

        // check for ship placement that overlaps
        public bool IsValidPlacement(List<string> location)
        {
            bool result = false;
            if (shipBoard == null)
            {
                List<string> shipBoard = new List<string>();
                result = true;
            }
            foreach (List<string> shipList in shipBoard)
            {
                foreach (string ship in shipList)
                {
                    if (!location.Contains(ship))
                    {
                        result = true;
                    }

                    else
                    {
                        Console.WriteLine("One of the coordinates is intersecting with another ship.");
                        Console.WriteLine("\nPlease try again");
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        // prints the current state of ShipBoard
        public void PrintShipBoard(FiringBoard firingBoard)
        {
            char[,] board = DisplayShipBoard(firingBoard);

            for (int i = 0; i < board.Length; i++)
            {
                List<string> tempBoardList = new List<string>();
                for (int j = 0; j < board.GetLength(i); j++)
                {
                    tempBoardList.Add(Color(board[i, j]));
                }
                Console.WriteLine(tempBoardList);
            }
        }

        //    foreach (char[] chars in board)
        //    {
        //        List<string> tempBoardList = new List<string>();
        //        foreach (char c in chars)
        //        {
        //            tempBoardList.Add(Color(c));
        //        }
        //        Console.WriteLine(tempBoardList);
        //    }
        //}

        // TODO: This is in here twice. Fix that
        private char[,] DisplayShipBoard(FiringBoard firingBoard)
        {
            char[,] board = new char[11,11]
            {
                {'*', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', },
                {'a', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
                {'b', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
                {'c', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
                {'d', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
                {'e', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
                {'f', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
                {'g', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
                {'h', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
                {'i', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
                {'j', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', },
            };

            if (shipBoard != null)
            {
                foreach (List<string> boat in shipBoard)
                {
                    foreach (string b in boat)
                    {
                        char row = b[0];
                        char col = b[1];
                        int colInt = (col - '1') + 1;
                        int rowInt = (row - 'a' + 1);
                        board[rowInt, colInt + 1] = 'S';
                    }
                }
            }

            if (firingBoard.GetFiringBoardHits() != null)
            {
                foreach (string record in firingBoard.GetFiringBoardHits())
                {
                    char row = record[0];
                    char col = record[1];
                    int colInt = (col - '1') + 1;
                    int rowInt = (row - 'a' + 1);
                    board[rowInt, colInt + 1] = 'X';
                }
            }
            return board;
        }

        // changing the font color for the board
        private string Color(char grid)
        {
            string resetColor = "\u001B[0m";
            string red = "\u001B[31m";
            string blue = "\u001B[36m";
            string gray = "\u001B[90m";

            switch (grid)
            {
                case '-':
                    return blue + grid + resetColor;
                case 'X':
                    return red + grid + resetColor;
                case 'S':
                    return gray + grid + resetColor;
                default:
                    string v = grid.ToString();
                    return v;
            }
        }

        // check if there are ships remaining
        public bool AllShipsSunk()
        {
            bool result = false;
            if (shipBoard == null || shipBoard.Count() == 0)
            {
                result = true;
            }

            return result;
        }

        public List<List<string>> GetShipBoard() { return shipBoard; }

    }
}
