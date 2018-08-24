using Hangman.Decision;
using Hangman.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.ViewController
{
    public class Controller
    {
        private GameData m_gameData;
        private DecisionMaker m_decisionMaker;

        public Controller()
        {

        }

        public void InitGameData(int wordLength)
        {
            m_gameData = new GameData(wordLength);
            m_decisionMaker = new Distribution(m_gameData);
        }

        /// <summary>
        /// checks if the game is finished
        /// </summary>
        /// <returns></returns>
        public bool IsFinished()
        {
            return m_gameData.m_possibleWords.Count == 1 || m_gameData.m_possibleWords.Count == 0 || !m_gameData.m_currentWord.Contains('_');
        }

        public void MakeStep(string newWord, char predictedChar)
        {
            m_gameData.UpdateWord(newWord, predictedChar);
        }

        public HashSet<char> GetAlreadyPredictedChars()
        {
            return m_gameData.m_predictedChars;
        }

        public Dictionary<char, float> GetPercentages()
        {
            return m_decisionMaker.GetPercentages();
        }

        /// <summary>
        /// searches for the best percentage in dictionary
        /// </summary>
        /// <param name="charPercentageDic"></param>
        /// <returns></returns>
        public KeyValuePair<char, float> GetBestPair()
        {
            KeyValuePair<char, float> best = new KeyValuePair<char, float>('-', 0.0f);

            foreach (KeyValuePair<char, float> val in GetPercentages())
            {
                if (val.Value > best.Value)
                {
                    best = val;
                }
            }

            return best;
        }

        public List<string> GetPossibleWords()
        {
            return m_gameData.m_possibleWords;
        }

        public string GetCurrentWord()
        {
            return m_gameData.m_currentWord;
        }
    }
}
