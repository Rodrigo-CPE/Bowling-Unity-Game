using System.Collections.Generic;

public static class ScoreMaster
{
    public static int Score(List<int> rolls)
    {
        int score = 0;
        int rollIndex = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (rollIndex >= rolls.Count)
                break;

            if (IsStrike(rolls, rollIndex))
            {
                if (rollIndex + 2 >= rolls.Count)
                    break;

                score += 10 + rolls[rollIndex + 1] + rolls[rollIndex + 2];
                rollIndex += 1;
            }
            else
            {
                if (rollIndex + 1 >= rolls.Count)
                    break;

                int frameScore = rolls[rollIndex] + rolls[rollIndex + 1];

                if (frameScore == 10)
                {
                    if (rollIndex + 2 >= rolls.Count)
                        break;

                    score += 10 + rolls[rollIndex + 2];
                }
                else
                {
                    score += frameScore;
                }

                rollIndex += 2;
            }
        }

        return score;
    }

    static bool IsStrike(List<int> rolls, int index)
    {
        return index < rolls.Count && rolls[index] == 10;
    }
}