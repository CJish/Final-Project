

using System.Drawing;

namespace BattleShip.Models
{
    public class ShipBoard
    {
        private List<List<string>> shipBoard;

        public void PlaceShip(List<string> location)
        {
            if (shipBoard == null)
            {
                List<string> shipBoard = new List<string>();
            }
            shipBoard.Add(location);
        }

        // check to see if ship is sunk
        public bool Sink()
        {
            bool result = false;
            for (List<string> boat : shipBoard)
            {
                if (boat.isEmpty())
                {
                    shipBoard.remove(boat);
                    result = true;
                    Console.ReadLine(); //pause
                    break;
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
            for (List<string> shipList : shipBoard)
            {
                for (string ship : shipList)
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
            char[][] board = DisplayShipBoard(firingBoard);
            for (char[] chars : board)
            {
                List<string> tempBoardList = new List<string>();
                for (char c : chars)
                {
                    tempBoardList.Add(color(c));
                }
                Console.WriteLine(tempBoardList);
            }
        }

        // The shipboard
        private char[][] DisplayShipBoard(FiringBoard firingBoard)
        {
            char[][] board =
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
                for (List<string> boat : shipBoard)
                {
                    for (string b : boat)
                    {
                        char row = b.chartAt(0);
                        char col = b.chartAt(1);
                        int colInt = (col - '1') + 1;
                        int rowInt = (row - 'a' + 1);
                        board[rowInt][colInt + 1] = 'S';
                    }
                }
            }

            if (firingBoard.GetFiringBoardHits() != null)
            {
                for (string record : firingBoard.GetFiringBoardHits())
                {
                    char row = record.chartAt(0);
                    char col = record.chartAt(1);
                    int colInt = (col - '1') + 1;
                    int rowInt = (row - 'a' + 1);
                    board[rowInt][colInt + 1] = 'X';
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
                    string v = char.GetNumericValue(grid); // String.valueOf(grid)
                    return v;
            }
        }

        // check if there are ships remaining
        public bool AllShipsSunk()
        {
            bool result = false;
            if (shipBoard == null || shipBoard.size() == 0)
            {
                result = true;
            }

            return result;
        }

        public List<List<string>> GetShipBoard() { return shipBoard; }

    }
}
