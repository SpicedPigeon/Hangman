using Hangman.ViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Decision
{
    public abstract class DecisionMaker
    {
        protected GameData m_gameData;

        public DecisionMaker(GameData gd)
        {
            m_gameData = gd;
        }

        public abstract char GivePrediction();
        public abstract Dictionary<char, float> GetPercentages();
    }
}
