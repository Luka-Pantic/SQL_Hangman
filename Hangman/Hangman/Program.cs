using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;
using MySql.Data.MySqlClient;
using System.Diagnostics.Tracing;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Set up MySql Connection
            string connectionString = "server = 127.0.0.1; database = Hangman; uid = root; password = 123456;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //Add words from Database to List

            List<string> words = new List<string>();
            string query = "SELECT begriff FROM woerter";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string word = reader.GetString(0);
                words.Add(word);
            }

            reader.Close();






           
            Random random = new Random();
            string secretWord = words[random.Next(words.Count)];
            int maxGuesses = 6; // maximum number of incorrect guesses allowed
            int incorrectGuesses = 0;
            char[] guessedLetters = new char[secretWord.Length]; // array to store correctly guessed letters
            bool gameWon = false;
            int hangmanStage = 0;




            List<string> hangmanImages = new List<string>
            {
                 @"   +---+
                |   |
                    |
                    |
                    |
                    |
             =========",


                 @"   +---+
                |   |
                O   |
                    |
                    |
                    |
             =========",


                 @"   +---+
                |   |
                O   |
                |   |
                    |
                    |
             =========",



                 @"   +---+
                |   |
                O   |
                |   |
               /|   |
                    |
             =========",



                 @"   +---+
                |   |
                O   |
                |   |
               /|\  |
                    |
             =========",



                 @"   +---+
                |   |
                O   |
                |   |
               /|\  |
               /    |
             =========",


                 @"   +---+
                |   |
                O   |
                |   |
               /|\  |
               / \  |
             =========",

            };




            void DisplayHangman()
            {
                Console.WriteLine(hangmanImages[hangmanStage]);
            }






            for (int i = 0; i < guessedLetters.Length; i++)
            {
                guessedLetters[i] = '_';
            }

            while (incorrectGuesses < maxGuesses && !gameWon)
            {
                Console.WriteLine("Rate einen Buchstaben:");
                char guess = Console.ReadKey().KeyChar;
                Console.WriteLine();

                bool correctGuess = false;
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (secretWord[i] == guess)
                    {
                        guessedLetters[i] = guess;
                        correctGuess = true;
                    }
                }

                if (correctGuess)
                {
                    Console.WriteLine("Richtig!");
                }
                else
                {
                    Console.WriteLine("Falsch!");
                    incorrectGuesses++;
                    hangmanStage++;
                    DisplayHangman();

                }

                Console.WriteLine("Aktueller Stand des Wortes: " + new string(guessedLetters));

                if (new string(guessedLetters) == secretWord)
                {
                    Console.WriteLine("Super, du hast gewonnen!");
                    gameWon = true;
                }
            }

            if (!gameWon)
            {
                Console.WriteLine("Schade, du hast verloren! Das Wort war: " + secretWord);
            }

            Console.ReadLine(); 






        }




    }
}
    
        