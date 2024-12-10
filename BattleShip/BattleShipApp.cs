using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using BattleShip.Models; // TODO figure this out

namespace BattleShip
{
    public class BattleShipApp
    {
        private PlayerModel player1 = new PlayerModel();
        private PlayerModel player2 = new CPUPlayer();
        private static ShipBoard player1ShipBoard = new ShipBoard(); // had to make static so I could initialize it
        private static ShipBoard player2ShipBoard = new ShipBoard();
        private FiringBoard player1FiringBoard = new FiringBoard(player2ShipBoard);
        private FiringBoard player2FiringBoard = new FiringBoard(player1ShipBoard);
        private bool isCPUPlaying = false;
        private string bannerDirectory = "./filelocation";

        // Let's instantiate the board and players:
        public void BattleShipGame() //throws IOException
        {
            ShowBanner("banner.txt");
            player1 = new PlayerModel();
            player2 = new CPUPlayer();

        }

        // start the game with an option to view instructions
        public void StartGame()
        {
            Console.WriteLine("Do you want to see the instructions?");
            Console.WriteLine("Please press y for yes or n for no");
            string response = Console.ReadLine().ToLower().Trim();
            if (response.Equals("y") || response.Equals("yes"))
            {
                showTutorial();
                Console.WriteLine("Hit the 'enter' key to continue");
                response = Console.ReadLine();
                Console.Clear();
            }
            Console.Clear();
            PlaceShips();
            PlayRounds();
        }

        private void ShowBanner(string bannerFileName) // throws IOexception?
        {
            string path = "./" + bannerFileName;
            File.ReadAllLines(path);
        }

        // place the ships on the board
        private void PlaceShips() // throws IOException?
        {
            Console.WriteLine("Player 1:");
            Console.WriteLine("\tPlace your ships on the board");
            player1.PlaceShips(player1ShipBoard, player2FiringBoard);
            Console.Clear(); // TODO: clearConsole()?

            Console.WriteLine("Player2:");
            Console.WriteLine("\tPlace your ships on the board");
            player2.PlaceShips(player2ShipBoard, player1FiringBoard);
            Console.Clear();
        }

        // Manage the rounds
        public void PlayRounds() // Throws IOEXception?
        {
            while (!player1ShipBoard.AllShipsSunk() && !player2ShipBoard.AllShipsSunk())
            {
                takeTurns(player1, player2);
            }
        }

        // Manage the players' turns
        private void takeTurns(PlayerModel player1, PlayerModel player2)
        {
            while (!player1ShipBoard.AllShipsSunk() && !player2ShipBoard.AllShipsSunk())
            {
                Console.WriteLine("Player 1 Ship Board:");
                player1ShipBoard.PrintShipBoard(player2FiringBoard);

                Console.WriteLine("Player 1 Firing Board:");
                player1FiringBoard.PrintFiringBoard();

                Console.WriteLine("Player 1's turn to fire:");
                player1FiringBoard.Fire(player1.TakeTurn(player1FiringBoard, player2ShipBoard));

                if (!player1FiringBoard.Impact(PlayerModel.GetGuess()))
                {
                    Console.WriteLine("You missed!");
                    Console.ReadLine(); // just pausing the action here
                }

                if (player1FiringBoard.Impact(CPUPlayer.GetGuess()))
                {
                    Console.WriteLine("You hit a ship!");
                    Console.ReadLine();
                }

                if (player2ShipBoard.Sink())
                {
                    Console.WriteLine("You sunk a ship!");
                    Console.ReadLine();
                }

                if (player2ShipBoard.AllShipsSunk())
                {
                    ShowBanner("end_game_banner.txt");
                    Console.WriteLine("Player 1 won the game!");
                    break;
                }
                Console.Clear();

                Console.WriteLine("Player 2's Ship Board");
                player2ShipBoard.PrintShipBoard(player1FiringBoard);

                Console.WriteLine("Player 2's Firing Board");
                player2FiringBoard.PrintFiringBoard();

                Console.WriteLine("Player 2's turn to fire");
                player2FiringBoard.Fire(player2.TakeTurn(player2FiringBoard, player1ShipBoard));

                if (player1ShipBoard.Sink())
                {
                    Console.WriteLine("Player 1's ship has been sunk!");
                    Console.ReadLine();
                }
                if (player1ShipBoard.AllShipsSunk())
                {
                    ShowBanner("end_game_banner.txt");
                    Console.WriteLine("Player 2 has won!");
                    break;
                }
                Console.Clear();
            }
        }

        public void showTutorial() // throws IO?
        {
            Console.WriteLine("Each of the two players has their own board consisting of a 10 x 10 grid\n");
            Console.WriteLine("The game board will use '-' to represent water");
            Console.ReadLine();
            Console.WriteLine("Each player places all of their ships on their board");
            Console.ReadLine();
            Console.WriteLine("Ships can be placed horizontally or vertically, so long as the entire ship is on the board and ships do not overlap\n");
            Console.WriteLine("Ships on the game board will be represented by 'S'");
            Console.ReadLine();
            Console.WriteLine("The players then take turns firing at their opponent's ships by typing in grid coordinates (e.g. C5 or I10");
            Console.WriteLine("A hit is marked by 'H' while misses are marked by 'M' on the game board");
            Console.ReadLine();
            Console.WriteLine("A ship is 'sunk' when each grid square that it occupies has been hit");
            Console.ReadLine();
            Console.WriteLine("The goal of the game is to sink ALL of the opponent's ships");
            Console.ReadLine();
            Console.WriteLine("May the seas be in your favor!");
        }

        private void clearConsole()
        {
            if (isCPUPlaying)
            {
                Console.Clear();
                ShowBanner("switching_player_banner.txt");
                Console.Clear();
            }
            else
            {
                Console.Clear();
                ShowBanner("switching_player_banner.txt");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}