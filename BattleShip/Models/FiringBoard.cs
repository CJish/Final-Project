

using System.Collections;
using System.Drawing;
using System.Runtime;

namespace BattleShip.Models
{
    public class FiringBoard
    {
        private List<string> fireRecord;
        private List<string> firingBoardHits;
        private ShipBoard shipBoard;
        public string cpuHit;

        public FiringBoard(ShipBoard shipBoard) { this.shipBoard = shipBoard; }

        // fire a round
        public void Fire(string coords)
        {
            if (fireRecord == null)
            {
                firingBoardHits = new List<string>();
                fireRecord = new List<string>();
                fireRecord.Add(coords);
                Impact(coords);
            }
            else
            {
                for (string shot : fireRecord)
                {
                    if (coords.equals(shot))
                    {
                        Console.WriteLine("You already shot at that same location.");
                        Console.WriteLine("\tBut you shoot at it again with the same result");
                        break;
                    }
                    else
                    {
                        fireRecord.add(coords);
                        impact(coords);
                        break;
                    }
                }
            }
        }

        // recording the shots taken
        public bool Impact(string coords)
        {
            bool result = false;
            for (List<string> boat : shipBoard.GetShipBoard()) // foreach
            {
                for (string b : boat) // foreach
                {
                    if (b.equals(coords))
                    {
                        firingBoardHits.add(coords);
                        fireRecord.add(coords);
                        Console.WriteLine($"That round hit a ship! {coords}");
                        Console.ReadLine();
                        hit(coords);
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        // successfult hit takes out a spot on the grid
        private void Hit(string coords)
        {
            for (List<string> boat : shipBoard.GetShipBoard())
            {
                cpuHit = coords;
                boat.remove(coords);
            }
        }

        // updates the state of the firing board
        public void PrintFiringBoard()
        {
            char[][] board = DisplayFiringBoard();
            for (char[] chars : board)
            {
                List<string> tempBoardList = new List<string>();
                for (char c : chars)
                {
                    tempBoardList.add(color(c));
                }
                Console.WriteLine(tempBoardList);
            }
        }

        // TODO how is this different than the ShipBoard method?
        private char[][] DisplayFiringBoard()
        {
            char[][] board =
            {
                {'*', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',},
                {'a', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
                {'b', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
                {'c', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
                {'d', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
                {'e', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
                {'f', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
                {'g', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
                {'h', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
                {'i', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
                {'j', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-',},
            };

            if (fireRecord != null && firingBoardHits != null)
            {
                for (String record : fireRecord)
                {
                    char row = record.charAt(0);
                    char col = record.charAt(1);
                    int colInt = (col - '1') + 1;
                    int rowInt = (row - 'a' + 1);
                    board[rowInt][colInt + 1] = 'M';
                }

                for (String record : firingBoardHits)
                {
                    char row = record.charAt(0);
                    char col = record.charAt(1);
                    int colInt = (col - '1') + 1;
                    int rowInt = (row - 'a' + 1);
                    board[rowInt][colInt + 1] = 'H';
                }
            }
            return board;
        }

        private string Color(char grid) // how is this different than ShipBoard?
        {
            String resetColor = "\u001B[0m";
            String red = "\u001B[31m";
            String blue = "\u001B[36m";
            String gray = "\u001B[90m";

            switch (grid)
            {
                case '-':
                    return blue + grid + resetColor;
                case 'H':
                    return red + grid + resetColor;
                case 'M':
                    return gray + grid + resetColor;
                default:
                    String v = String.valueOf(grid);
                    return v;
            }
        }

        public List<string> GetFireRecord() { return fireRecord; }

        public List<string> GetFiringBoardHits() { return firingBoardHits; }

        public string GetCpuHit() { return cpuHit; }
    }
}
