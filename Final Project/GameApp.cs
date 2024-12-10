using BattleShip.BattleShipApp;

namespace Final_Project
{
    internal class GameApp
    {
        

        public static void Start()
        {
            string userchoice;

            Console.Clear(); // clearing the screen so if the user returns from another game they'll get the menu screen

            Console.WriteLine("Hello");
            Console.WriteLine("Let's play some games\n");
            Console.WriteLine("Press 1 to play Blackjack");
            Console.WriteLine("Press 2 to play Hangman");
            Console.WriteLine("Press 3 to play Battleship");

            userchoice = Console.ReadLine();

            int cleanChoice = 0;

            while(!int.TryParse(userchoice, out cleanChoice))
            {
                Console.WriteLine("That's not a valid choice, please try again");
                userchoice = Console.ReadLine();
            }

            switch (cleanChoice)
            {
                case 1:
                    Blackjack.runBlackjack();
                    break;
                case 2:
                    Hangman.runHangman();
                    break;
                case 3:
                    BattleShipApp.BattleShipGame();
                default:
                    Console.WriteLine("I don't think that was a valid option. Press any key to try again.");
                    Console.ReadLine();
                    Start();
                    break;
            }
        }
    }
}
