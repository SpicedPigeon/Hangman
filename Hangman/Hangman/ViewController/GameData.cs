using Hangman.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.ViewController
{
    public class GameData
    {
        public string m_currentWord;
        public int m_wordLength;
        public List<string> m_possibleWords;
        public HashSet<char> m_predictedChars;

        public GameData(int wordLength)
        {
            m_predictedChars = new HashSet<char>();
            m_wordLength = wordLength;
            m_possibleWords = new List<string>();
            foreach (string s in FileReader.GetInstance().lengthWordArray[wordLength])
            {
                m_possibleWords.Add(s);
            }
        }

        /// <summary>
        /// Updates the word and refreshes the list of possible words
        /// </summary>
        /// <param name="aCurrentWord"></param>
        /// <param name="predictedChar"></param>
        public void UpdateWord(string aCurrentWord, char predictedChar)
        {
            m_predictedChars.Add(predictedChar);
            m_currentWord = aCurrentWord;

            List<string> newPossibleWords = new List<string>();

            //if char isnt in new word -> char was wrong -> kick all words with that char
            if (!m_currentWord.Contains(predictedChar))
            {
                foreach (string word in m_possibleWords)
                {
                    if (!word.Contains(predictedChar))
                    {
                        newPossibleWords.Add(word);
                    }
                }
                m_possibleWords = newPossibleWords;
                newPossibleWords = new List<string>();
            }

            //kick words not having same letter at same pos or having already predicted letters not at the same pos
            foreach (string tmpWord in m_possibleWords)
            {
                bool validWord = true;
                for (int i = 0; i < m_wordLength; i++)
                {
                    if ((!m_currentWord[i].Equals('_') && !tmpWord[i].Equals(m_currentWord[i])) || (m_currentWord[i].Equals('_') && tmpWord[i].Equals(predictedChar)))
                    {
                        validWord = false;
                        break;
                    }
                }
                if (validWord)
                {
                    newPossibleWords.Add(tmpWord);
                }
            }
            m_possibleWords = newPossibleWords;

            //if char exits in every word ->at same position<- remove it only if its the only letter not if it is twice in the same word
            for (int i = 0; i < m_currentWord.Length; i++)
            {
                if (m_currentWord[i].Equals('_'))
                {
                    bool allWordsSameLetterSamePos = true;
                    char c = m_possibleWords[0][i];
                    foreach (string posWord in m_possibleWords)
                    {
                        if (!posWord[i].Equals(c))
                        {
                            Console.WriteLine("not same letter: " + posWord[i] + " letter was: " + c);
                            allWordsSameLetterSamePos = false;
                            break;
                        }
                    }
                    if (allWordsSameLetterSamePos)
                    {
                        Console.WriteLine("same letter: " + c);
                        m_predictedChars.Add(c);
                    }
                }
            }
        }
    }
}
