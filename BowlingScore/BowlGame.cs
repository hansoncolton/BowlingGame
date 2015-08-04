

namespace BowlingScore
{
    public class BowlGame 
    {
        private int[] rolls = new int[21];
        public int currentRoll = 0;
        public bool ignoreCallToForm;
        public int Score
        {
            get
            {
                int score =0;
                int rollIndex = 0;
                for (var frame = 0; frame < 10; frame++)
                {
                    var frameScore = 0;
                    if (IsStrike(rollIndex))
                    {
                        frameScore = ScoreStrike(rollIndex);
                        rollIndex ++;
                 if(!ignoreCallToForm) BowlingForm.bForm.SetFrameDisplay(frame+1, "X");
                    }
                    else if (IsSpare(rollIndex))
                    {
                        frameScore = ScoreSpare(rollIndex);
                        if (!ignoreCallToForm) BowlingForm.bForm.SetFrameDisplay(frame + 1, rolls[rollIndex].ToString() + " /");
                        rollIndex += 2;
                    }
                    else
                    {
                        frameScore = GetRegularScore(rollIndex);
                        if (!ignoreCallToForm) BowlingForm.bForm.SetFrameDisplay(frame + 1, rolls[rollIndex].ToString() + " " + rolls[rollIndex + 1].ToString());
                        rollIndex += 2;
                    }
                    score += frameScore;
                    if (!ignoreCallToForm) BowlingForm.bForm.SetTotalForFrame(frameScore, frame + 1);
                }
                
                return score;
            }
        }

        public void Roll(int Pins)
        {
            rolls[currentRoll++] = Pins;
        }
        
        private bool IsStrike(int rollIndex)
        {
            return rolls[rollIndex] == 10;
        }

        private int ScoreStrike(int rollIndex)
        {
            return rolls[rollIndex] + rolls[rollIndex + 1] + rolls[rollIndex + 2];
        }

        private bool IsSpare(int rollIndex)
        {
            return rolls[rollIndex] + rolls[rollIndex + 1] == 10;
        }

        private int ScoreSpare(int rollIndex)
        {
            return rolls[rollIndex] + rolls[rollIndex + 1] + rolls[rollIndex + 2];
        }

        private int GetRegularScore(int rollIndex)
        {
            return rolls[rollIndex] + rolls[rollIndex + 1];
        }

    }
}
