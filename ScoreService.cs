using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadriJong
{

    public interface IScoreService
    {
        int Score { get; }
        void IncreaseScore(int points);
        void ResetScore();
    }

    public class ScoreService : IScoreService
    {
        public int Score { get; private set; }

        public void IncreaseScore(int points)
        {
            Score += points;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
