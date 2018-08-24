using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.ViewController
{
    public class UI
    {
        private const int MAX_COUNT_WORDS_TO_PRINT = 50;
        private Controller m_controller;

        private int m_wordLength;
        
        public UI()
        {
            m_controller = new Controller();
        }

        /// <summary>
        /// Reads the length of the word at the start
        /// </summary>
        public void ReadWordLength()
        {
            string wordLength = "";

            while (!ValidLength(wordLength))
            {
                Console.WriteLine("Write down the length of your word:");
                wordLength = Console.ReadLine();
            }
            int length = -1;
            Int32.TryParse(wordLength, out length);

            m_wordLength = length;
            m_controller.InitGameData(m_wordLength);
        }

        /// <summary>
        /// Checks if the given word is valid
        /// </summary>
        /// <param name="lengthInputString"></param>
        /// <returns></returns>
        private bool ValidLength(string lengthInputString)
        {
            if (lengthInputString.Length >= 1)
            {
                foreach (char c in lengthInputString)
                {
                    //valid char
                    if (c > '9' || c < '0')
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            //not "0"
            int wordLength = -1;
            Int32.TryParse(lengthInputString, out wordLength);
            if (wordLength == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Prints the possible words if they are less than const
        /// </summary>
        /// <param name="updatedWord"></param>
        private void ShowPossibleWords()
        {
            List<string> possibleWords = m_controller.GetPossibleWords();
            if (possibleWords.Count < MAX_COUNT_WORDS_TO_PRINT)
            {
                Console.WriteLine("Possible words: ");
                foreach (string word in possibleWords)
                {
                    Console.WriteLine(word);
                }
            }
            Console.WriteLine("Number of possible words: " + possibleWords.Count);
            Console.WriteLine("-------------------------------");
        }

        public bool ReadNewInput()
        {
            ShowPossibleWords();
            ShowPercentages();
            char c = ReadGuessedChar(m_controller.GetAlreadyPredictedChars());
            string updatedWord = ReadUpdatedWord(m_wordLength);
            m_controller.MakeStep(updatedWord, c);
            
            if (m_controller.IsFinished())
            {
                List<string> possibleWords = m_controller.GetPossibleWords();
                if (possibleWords.Count == 0)
                {
                    Console.WriteLine("Word doesn't exist in database!");
                }
                else
                {
                    Console.WriteLine("Finished! The word is " + possibleWords[0]);
                }
            }

            return m_controller.IsFinished();
        }

        private char ReadGuessedChar(HashSet<char> predictedChars)
        {
            string predictedCharInput = "";
            while (predictedCharInput.Length != 1 || predictedChars.Contains(predictedCharInput[0]))
            {
                Console.WriteLine("Write down your guessed letter:");
                predictedCharInput = Console.ReadLine();
            }

            return predictedCharInput[0];
        }

        private string ReadUpdatedWord(int wordLength)
        {
            string currentWord = "";

            while (currentWord.Length != wordLength)
            {
                Console.WriteLine("Write down the word with \"_\" as empty letters:");
                currentWord = Console.ReadLine();
            }

            return currentWord;
        }

        /// <summary>
        /// shows the percentages of each char
        /// </summary>
        private void ShowPercentages()
        {
            //calc percentages
            float gesamtProzentzahl = 0.0f;

            foreach (KeyValuePair<char, float> val in m_controller.GetPercentages())
            {
                gesamtProzentzahl += val.Value;
                Console.WriteLine("Character " + val.Key + " percentage: " + val.Value);
            }
            KeyValuePair<char, float> best = m_controller.GetBestPair();

            Console.WriteLine("All percentages added up: " + gesamtProzentzahl);
            Console.WriteLine("Best character: " + best.Key + " with a percentage of: " + best.Value);
            Console.WriteLine("-------------------------------");
        }
    }
}
