using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    [Header("Power-ups guardados")]
    public PowerUpType storedPowerUpP1 = PowerUpType.None;
    public PowerUpType storedPowerUpP2 = PowerUpType.None;

    void Awake()
    {
        Instance = this;
    }

    public void CollectRandomPowerUp(int player)
    {
        PowerUpType randomPowerUp = GetRandomPowerUp();

        if (player == 1)
            storedPowerUpP1 = randomPowerUp;
        else
            storedPowerUpP2 = randomPowerUp;

        Debug.Log($"Jogador {player} apanhou power-up: {randomPowerUp}");
    }

    public PowerUpType GetPowerUpToApplyOnPlayer(int player)
    {
        // Se é a vez do player 1, aplica o power-up guardado pelo player 2.
        if (player == 1)
        {
            PowerUpType power = storedPowerUpP2;
            storedPowerUpP2 = PowerUpType.None;
            return power;
        }
        else
        {
            PowerUpType power = storedPowerUpP1;
            storedPowerUpP1 = PowerUpType.None;
            return power;
        }
    }

    PowerUpType GetRandomPowerUp()
    {
        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0: return PowerUpType.Ice;
            case 1: return PowerUpType.Mud;
            case 2: return PowerUpType.SmallBall;
            case 3: return PowerUpType.Obstacles;
            default: return PowerUpType.None;
        }
    }
}