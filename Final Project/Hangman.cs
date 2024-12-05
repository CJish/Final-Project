using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;

namespace Final_Project
{
    internal class Hangman
    {
        // TODO: It doesn't seem to work right - the hangman picture might not be updating with every wrong letter and the game definitely gives the "you win" message prior to the user entering the correct answers.
        // TODO: Instead of quitting the program, I want it to ask if you want to play again and, if not, go back to the title screen where you can choose a different game
        // TODO: I'd recommend putting some console clearing in here
        // TODO: why is it that sometimes it shows the hangman and sometimes it doesn't?
        // TODO: why are the letters that I've guessed all over the place?
        // TODO: What's with the 8 lines of - in there?
        // TODO: Read a random word from a JSON or .txt file that has way more than just 9 words (but keep those easy ones for troubleshooting)
        // TODO: implement a high score system that writes to a file and give the user a way to check the high scores
        public static void runHangman()
        {
            Console.WriteLine("Time to play hangman!!");
            Console.WriteLine("-----------------------------------------");

            Random random = new Random();
            List<string> wordDictionary = new List<string> { "final", "alaska", "keyboard", "computer", "shoot", "dunk", "blue", "white", "basketball" };
            int index = random.Next(wordDictionary.Count);
            String randomWord = wordDictionary[index];

            foreach (char x in randomWord)
            {
                Console.Write("_ ");
            }

            int lengthOfWordToGuess = randomWord.Length;
            int amountOfTimesWrong = 0;
            List<char> currentLettersGuessed = new List<char>();
            int currentLettersRight = 0;

            while (amountOfTimesWrong != 6 && currentLettersRight != lengthOfWordToGuess) // TODO there has to be a better way to check if the user guessed the correct characters
            {
                Console.WriteLine("\nLetters guessed so far: ");
                foreach (char letter in currentLettersGuessed)
                {
                    Console.WriteLine(letter + " ");
                }
                // Ask user for input
                Console.WriteLine("\nGuess a letter: ");
                char letterGuessed = Console.ReadLine()[0];
                // Check if the guessed letter has already been guessed earlier
                if (currentLettersGuessed.Contains(letterGuessed))
                {
                    Console.WriteLine("\r\n You have already guessed this letter");
                    printHangman(amountOfTimesWrong);
                    currentLettersRight = printWord(currentLettersGuessed, randomWord);
                    printLines(randomWord);
                }
                else
                {
                    // Check if letter is in the randomWord
                    bool right = false;
                    for (int i = 0; i < randomWord.Length; i++) { if (letterGuessed == randomWord[i]) { right = true; } }

                    // IF the User is right
                    if (right)
                    {
                        printHangman(amountOfTimesWrong);
                        // Print word
                        currentLettersGuessed.Add(letterGuessed);
                        currentLettersRight = printWord(currentLettersGuessed, randomWord);
                        Console.WriteLine("\r\n");
                        printLines(randomWord);
                    }
                    // User was wrong
                    else
                    {
                        amountOfTimesWrong += 1;
                        currentLettersGuessed.Add(letterGuessed);
                        // Update the hangman
                        printHangman(amountOfTimesWrong);
                        // Print the random word (answer)
                        currentLettersRight = printWord(currentLettersGuessed, randomWord);
                        Console.WriteLine("\r\n");
                        printLines(randomWord);
                    }
                }
            }
            Console.WriteLine("\r\n Game over! Thank you for playing!!");
        }


        // TODO I'd recommend putting some console clearing in here somewhere
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

        private static int printWord(List<char> guessedLetters, String randomWord)
        {
            int counter = 0;
            int rightLetters = 0;
            Console.WriteLine("\r\n");
            foreach (char c in randomWord)
            {
                if (guessedLetters.Contains(c))
                {
                    Console.WriteLine(c + " ");
                    rightLetters += 1;
                }
                else
                {
                    Console.Write("  ");
                }
                counter += 1;
            }

            return rightLetters;
        }

        private static void printLines(String randomWord)
        {
            Console.WriteLine("\r");
            foreach (char c in randomWord)
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.WriteLine("\u0305");
            }
        }
    }
}
