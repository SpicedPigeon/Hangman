using Hangman.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.ViewController
{
    public class TestInterface
    {
        private string fullWord;
        private string currentWord;
        private int currentTrys;

        private int maxTrys;
        private int minTrys;
        private string maxWord;
        private string minWord;

        private int wordLengthUsed;

        private Controller m_controller;

        public TestInterface(int wordLengthUsed)
        {
            this.wordLengthUsed = wordLengthUsed;
            m_controller = new Controller();
            maxTrys = Int32.MinValue;
            minTrys = Int32.MaxValue;
            maxWord = "";
            minWord = "";
        }

        public void StartTest()
        {
            int countWord = 0;
            //Console.WriteLine("Word length: " + wordLengthUsed);
            for (int x = 0; x < FileReader.GetInstance().lengthWordArray[wordLengthUsed].Count; x++)
            {
                currentTrys = 0;
                currentWord = "";
                fullWord = FileReader.GetInstance().lengthWordArray[wordLengthUsed][x];
                m_controller.InitGameData(fullWord.Length);
                //Console.WriteLine(fullWord);
                for (int j = 0; j < fullWord.Length; j++)
                {
                    currentWord += "_";
                }
                bool finished = false;

                while (!finished)
                {
                    bool containedChar = false;
                    char guessed = m_controller.GetBestPair().Key;
                    string newWord = "";
                    //create string
                    for (int j = 0; j < fullWord.Length; j++)
                    {
                        if (fullWord[j].Equals(guessed))
                        {
                            newWord += fullWord[j];
                            containedChar = true;
                        }
                        else
                        {
                            newWord += currentWord[j];
                        }
                    }
                    currentWord = newWord;

                    m_controller.MakeStep(currentWord, guessed);
                    if (!containedChar)
                    {
                        currentTrys++;
                    }
                    finished = m_controller.IsFinished();
                }

                //min max update
                if (currentTrys < minTrys)
                {
                    minTrys = currentTrys;
                    minWord = fullWord;
                }
                if (currentTrys > maxTrys)
                {
                    maxTrys = currentTrys;
                    maxWord = fullWord;
                }
                countWord++;
                /*
                if (countWord % 1000 == 0)
                {
                    Console.WriteLine("number: " + countWord + " finished!");
                    Console.WriteLine("length: " + wordLengthUsed);
                    Console.WriteLine("min trys: " + minTrys + "\tword:" + minWord);
                    Console.WriteLine("max trys: " + maxTrys + "\tword:" + maxWord);
                }*/

            }
            if (countWord != 0)
            {
                Console.Write("-----------------" + "\nlength: " + wordLengthUsed + "\ncount: " + countWord + "\nfinal: min failed: " + minTrys + "\tword: " + minWord + "\nfinal: max failed: " + maxTrys + "\tword: " + maxWord + "\n");
            }
        }
    }
}
