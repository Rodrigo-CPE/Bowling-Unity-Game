using UnityEngine;

public static class GameResult
{
    public static int scoreP1;
    public static int scoreP2;
    public static int winner;

    public static void SetResult(int p1, int p2)
    {
        scoreP1 = p1;
        scoreP2 = p2;

        if (p1 > p2)
            winner = 1;
        else if (p2 > p1)
            winner = 2;
        else
            winner = 0;
    }
}