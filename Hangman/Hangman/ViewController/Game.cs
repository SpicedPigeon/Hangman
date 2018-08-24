using Hangman.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hangman.ViewController
{
    public class Game
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1250);
            int num = Int32.Parse(Console.ReadLine());
            if(num == 0)
            {
                UI userInterface = new UI();
                bool finished = false;
                int trys = 0;

                userInterface.ReadWordLength();

                while (!finished && trys <= 11)
                {
                    trys++;
                    finished = userInterface.ReadNewInput();
                }
                if (trys > 11)
                {
                    Console.WriteLine("You hang! You needed " + trys + " trys!");
                }
                else
                {
                    Console.WriteLine("You needed " + trys + " trys!");
                }
            }
            else
            {
                Parser.RefactoredFileReader refactoredFileReader = new Parser.RefactoredFileReader();
                refactoredFileReader.ReadData();
                List<Thread> threads = new List<Thread>();

                for(int i = 0; i < refactoredFileReader.lengthWordArray.Length; i++)
                {
                    TestInterface ti = new TestInterface(i);
                    Thread t = new Thread(new ThreadStart(ti.StartTest));
                    t.Start();
                    threads.Add(t);
                }

                foreach(Thread t in threads)
                {
                    t.Join();
                }
            }
            Console.WriteLine("all finished");
            Console.ReadLine();
        }
    }
}
