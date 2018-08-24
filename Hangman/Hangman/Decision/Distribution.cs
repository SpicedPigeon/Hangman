using Hangman.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Decision
{
    public class Distribution : DecisionMaker
    {
        private int m_countedLetters;

        public Distribution(GameData gd) : base(gd)
        {

        }

        public override char GivePrediction()
        {
            KeyValuePair<char, float> best = new KeyValuePair<char, float>('-', 0.0f);

            foreach (KeyValuePair<char, float> val in GetPercentages())
            {
                if (val.Value > best.Value)
                {
                    best = val;
                }
            }
            return best.Key;
        }

        /// <summary>
        /// converts the string to an char set
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private HashSet<char> GetCharSet(string word)
        {
            HashSet<char> chars = new HashSet<char>();

            foreach (char c in word.ToCharArray())
            {
                chars.Add(c);
            }

            return chars;
        }

        /// <summary>
        /// Counts the char in strings
        /// </summary>
        /// <param name="words"></param>
        /// <param name="predictedChars"></param>
        /// <returns></returns>
        private Dictionary<char, int> CountCharsInStrings()
        {
            Dictionary<char, int> countCharDic = new Dictionary<char, int>();
            m_countedLetters = 0;

            //Count letters
            foreach (string word in m_gameData.m_possibleWords)
            {
                HashSet<char> chars = GetCharSet(word);
                foreach (char c in chars)
                {
                    if (!m_gameData.m_predictedChars.Contains(c))
                    {
                        m_countedLetters++;
                        int val = 0;
                        countCharDic.TryGetValue(c, out val);
                        if (val != 0)
                        {
                            countCharDic.Remove(c);
                            countCharDic.Add(c, ++val);
                        }
                        else
                        {
                            countCharDic.Add(c, ++val);
                        }
                    }
                }
            }
            return countCharDic;
        }

        /// <summary>
        /// calculates the percentage per char relative to the number of chars
        /// </summary>
        /// <param name="countCharDic"></param>
        /// <returns></returns>
        public override Dictionary<char, float> GetPercentages()
        {
            Dictionary<char, int> countCharDic = CountCharsInStrings();

            Dictionary<char, float> percentages = new Dictionary<char, float>();

            foreach (char key in countCharDic.Keys)
            {
                int countChar;
                countCharDic.TryGetValue(key, out countChar);

                float charPercentage = (countChar * 1.0f) / (m_countedLetters);
                percentages.Add(key, charPercentage*100);
            }

            return percentages;
        }
    }
}
