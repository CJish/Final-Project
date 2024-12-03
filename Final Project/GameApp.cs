﻿

namespace Final_Project
{
    internal class GameApp
    {
        

        public static void Start()
        {
            string userchoice;

            Console.Clear(); // clearing the screen so if the user returns from another game they'll get the menu screen

            Console.WriteLine("Hello");
            Console.WriteLine("Let's play some games");
            Console.WriteLine("Press 1 to play Blackjack");

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
                default:
                    Console.WriteLine("I don't think that was a valid option. Press any key to try again.");
                    Console.ReadLine();
                    Start();
                    break;
            }
        }
    }
}