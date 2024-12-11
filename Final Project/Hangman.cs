using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;

namespace Final_Project
{
    internal class Hangman
    {
        public static void runHangman()
        {
            Console.Clear();
            Console.WriteLine("Time to play hangman!!");


            Random random = new Random();
            List<string> wordDictionary = new List<string> { "final", "alaska", "keyboard", "computer", "shoot", "dunk", "blue", "white", "basketball" };
            int index = random.Next(wordDictionary.Count);
            string randomWord = wordDictionary[index];

            List<char> currentLettersGuessed = new List<char>();
            int lengthOfWordToGuess = randomWord.Length;
            int amountOfTimesWrong = 0;
            int currentLettersRight = 0;

            while (amountOfTimesWrong < 6 && currentLettersRight < lengthOfWordToGuess)
            {
                Console.Clear(); // Clear console for each iteration
                printHangman(amountOfTimesWrong);
                currentLettersRight = printWord(currentLettersGuessed, randomWord); // Reset right letters count

                // Display guessed letters
                Console.WriteLine("\nLetters guessed so far: " + string.Join(" ", currentLettersGuessed));

                // Ask for user input
                Console.WriteLine("\nGuess a letter: ");
                char letterGuessed;
                string input = Console.ReadLine();

                if (input.Length == 1 && Char.IsLetter(input[0]))
                {
                    letterGuessed = input[0];
                }
                else
                {
                    Console.WriteLine("Please enter a valid letter.");
                    continue; // Skip 
                }

                // Check if the letter has been guessed before
                if (currentLettersGuessed.Contains(letterGuessed))
                {
                    Console.WriteLine("You already guessed this letter!");
                }
                else
                {
                    currentLettersGuessed.Add(letterGuessed);
                    if (randomWord.Contains(letterGuessed))
                    {
                        // Correct guess
                    }
                    else
                    {
                        // Incorrect guess
                        amountOfTimesWrong++;
                    }
                }

                // Check if the player win 
                if (currentLettersRight == lengthOfWordToGuess)
                {
                    Console.WriteLine("\nYou win!");
                    break;
                }
            }

            // If the game ends because of too many wrong guesses
            if (amountOfTimesWrong == 6)
            {
                Console.WriteLine("\nYou lost! The word was: " + randomWord);
            }

            // Ask if the player wants to play again!!!!
            Console.WriteLine("Do you want to play again? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                runHangman(); // Restart the game
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
            }
        }

        private static void printHangman(int wrong)
        {
            if (wrong == 0)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 1)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("O   |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 2)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("O   |");
                Console.WriteLine("|   |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 3)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 4)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 5)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/   |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 6)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O   |");
                Console.WriteLine("/|\\  |");
                Console.WriteLine("/ \\  |");
                Console.WriteLine("    ===");
            }
        }

        private static int printWord(List<char> guessedLetters, string randomWord)
        {
            int rightLetters = 0;
            foreach (char c in randomWord)
            {
                if (guessedLetters.Contains(c))
                {
                    Console.Write(c + " ");
                    rightLetters++;
                }
                else
                {
                    Console.Write("_ ");
                }
            }
            Console.WriteLine(); // Move to next line after printing the word
            return rightLetters;
        }
    }
}
