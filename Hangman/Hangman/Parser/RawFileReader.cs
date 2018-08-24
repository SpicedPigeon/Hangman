using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hangman.Parser
{
    public class RawFileReader : FileReader
    {
        public override void ReadData()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Console.WriteLine(path + "/../../../../../Data/alltitles\n");
            StreamReader sr = new StreamReader(path + "/../../../../../Data/alltitles");

            int wordCounter = 0;

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                int counter = 0;
                char currentCharacter = line[counter++];
                while (!currentCharacter.Equals('\t'))
                {
                    currentCharacter = line[counter++];
                }

                string word = line.Substring(counter);
                word = word.ToUpper();

                bool validWord = true;

                foreach (char character in word)
                {
                    if ((character > 'Z' && character != 'Ä' && character != 'Ö' && character != 'Ü') || character < 'A')
                    {
                        validWord = false;
                    }
                }

                //Doppelte kommen noch vor  dauert zu lange!

                if (validWord && !lengthWordArray[word.Length].Contains(word))
                {
                    try
                    {
                        lengthWordArray[word.Length].Add(word);
                        wordCounter++;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Fehler beim Lesen: ");
                        Console.WriteLine(word + " Länge: " + word.Length + " Wortnummer: " + wordCounter);
                    }
                }

            }
            sr.Close();
            countWordsRead = wordCounter;
            Console.WriteLine("Anzahl deutscher Wörter gelesen: " + wordCounter);

            Console.WriteLine("Schreibe neue Datei bei Pfad: ");

            path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Console.WriteLine(path + "/../../../../../Data/words");
            File.Delete(path + "/../../../../../Data/words");
            StreamWriter sw = new StreamWriter(path + "/../../../../../Data/words");

            foreach (List<string> l in lengthWordArray)
            {
                foreach (string s in l)
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
        }
    }
}
