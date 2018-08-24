using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hangman.Parser
{
    public class XMLFileParser
    {
        public void ParseXMLFile()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path += "/../../../../../Data/Test.xml";
            //XmlDocument doc = new XmlDocument();
            //doc.Load(path + "/../../../../../Data/XMLFile.xml");

            Console.WriteLine(path);

            // Loading from a file, you can also load from a stream
            XDocument xml = XDocument.Load(@path);


            // Query the data and write out a subset of contacts
            IEnumerable<string> query = from page in xml.Root.Descendants("{http://www.mediawiki.org/xml/export-0.10/}page")
                                        where page.Element("{http://www.mediawiki.org/xml/export-0.10/}title") != null && !((string)page.Element("{http://www.mediawiki.org/xml/export-0.10/}title")).Contains(":")
                                        select (string)page.Element("{http://www.mediawiki.org/xml/export-0.10/}title");

            //{http://www.mediawiki.org/xml/export-0.10/}page
            Console.WriteLine("Length: " + query.Count<string>());

            foreach (string s in query)
            {
                Console.WriteLine("Title is: " + s);
            }
            /*

            foreach (XElement xe in xml.Root.Nodes())
            {
                Console.WriteLine("name : " + xe.Name);
                foreach (XElement xe2 in xe.Nodes())
                {
                    Console.WriteLine("name : " + xe2.Value);
                }
            }


            foreach (XElement page in query)
            {
                Console.WriteLine("pagename: " + page.Name);
                //IEnumerable<XElement> pageStuff = page.Descendant("title");
                foreach(XElement pageElements in page.DescendantNodes())
                {

                    Console.WriteLine("Title: " + pageElements.Name);
                }
            }
            */
            /*
            XmlNode node = doc.DocumentElement.SelectSingleNode("/mediawiki/siteinfo/sitename");
            Console.WriteLine(node.Value);

            int wordCounter = 0;
            string word = "";

            if (word.Contains("<title>") && !word.Contains("MediaWiki:") && !word.Contains("Benutzer Diskussion:") && !word.Contains("Flexion:") && !word.Contains("Benutzer:") && !word.Contains("Wiktionary:") && !word.Contains("Reim:"))
            {
                int startIndex = word.IndexOf("<title>");
                int endIndex = word.IndexOf("</title>");


                word = word.Substring(startIndex + 7, endIndex - 7 - startIndex);
                word = word.ToUpper();
                bool validWord = true;
                if (c % 100000 <= 100)
                {
                    Console.WriteLine("line: " + c + " word: " + word);
                }

                foreach (char character in word)
                {
                    if ((character > 'Z' && character != 'Ä' && character != 'Ö' && character != 'Ü') || character < 'A')
                    {
                        validWord = false;
                    }
                }

                if (validWord && word.Length > 1)
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


        */
        }
    }
}
