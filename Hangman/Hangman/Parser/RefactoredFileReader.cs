using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Parser
{
    public class RefactoredFileReader : FileReader
    {
        public override void ReadData()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //Console.WriteLine(path + "/../../../../../Data/german");
            StreamReader sr = new StreamReader(path + "/../../../../../Data/german", Encoding.GetEncoding(1250));

            int wordCounter = 0;

            while (!sr.EndOfStream)
            {
                string word = sr.ReadLine();


                word = word.ToUpper();

                bool validWord = true;
                string newWord = "";

                foreach (char character in word)
                {
                    if (character != ' ')
                    {
                        newWord += character;
                    }
                }

                if (validWord)
                {
                    try
                    {
                        lengthWordArray[newWord.Length].Add(newWord);
                        wordCounter++;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error reading: ");
                        Console.WriteLine(word + " length: " + newWord.Length + " number: " + wordCounter);
                    }
                }
                else
                {
                    Console.WriteLine("invalid word: " + newWord);
                }

            }
            sr.Close();
            countWordsRead = wordCounter;
            //Console.WriteLine("Words read: " + wordCounter);
            //Console.WriteLine("-------------------------------");
        }
    }
}
