using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Parser
{
    public abstract class FileReader
    {
        public List<string>[] lengthWordArray;
        public int countWordsRead;
        private static FileReader instance;

        public FileReader()
        {
            lengthWordArray = new List<string>[70];
            for (int i = 0; i < lengthWordArray.Length; i++)
            {
                lengthWordArray[i] = new List<string>();
            }
            instance = this;
        }

        public static FileReader GetInstance()
        {
            return instance;
        }

        public abstract void ReadData();
    }
}
