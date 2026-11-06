using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PowerUpZoneSpawner powerUpZoneSpawner;

    [Header("Estado")]
    public int currentPlayer = 1;
    public int currentFrame = 1;
    public int currentRoll = 1;

    [Header("Referências")]
    public BallLauncher ballLauncher;
    public GameObject ballObject;
    public PinManager pinManager;

    [Header("Power-up Zones")]
    public GameObject mudZone;
    public GameObject iceZone;

    private Vector3 ballStartPosition;
    private Vector3 originalBallScale;
    private float originalBallMass;

    private List<int> rollsP1 = new List<int>();
    private List<int> rollsP2 = new List<int>();

    private int firstRollPins = 0;
    private bool gameEnded = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        AudioManager.Instance?.PlayGameMusic();

        ballStartPosition = ballObject.transform.position;
        originalBallScale = ballObject.transform.localScale;
        originalBallMass = ballObject.GetComponent<Rigidbody>().mass;

        if (mudZone != null)
            mudZone.SetActive(false);

        if (iceZone != null)
            iceZone.SetActive(false);

        currentPlayer = 1;
        currentFrame = 1;
        currentRoll = 1;

        pinManager.ResetPins();
        PrepareBall();

        powerUpZoneSpawner?.RollSpawn();

        Debug.Log("Vez do Jogador 1 — Frame 1 — Lançamento 1");
        UIManager.Instance?.ShowTurnMessage(currentPlayer);
    }

    public void Bowl(int pinsKnocked)
    {
        if (gameEnded) return;

        pinsKnocked = Mathf.Clamp(pinsKnocked, 0, 10);

        List<int> rolls = GetCurrentRolls();
        rolls.Add(pinsKnocked);

        Debug.Log($"Score atual P1: {GetScore(1)} | Score atual P2: {GetScore(2)}");
        Debug.Log($"P{currentPlayer} | Frame {currentFrame} | Roll {currentRoll} | Derrubou {pinsKnocked}");

        if (currentRoll == 1)
        {
            firstRollPins = pinsKnocked;

            if (pinsKnocked >= 10)
            {
                Debug.Log($"STRIKE! Jogador {currentPlayer}");
                AudioManager.Instance?.PlayStrike();
                VFXManager.Instance?.PlayStrikeVFX();
                Invoke(nameof(EndPlayerTurn), 3.5f);
            }
            else
            {
                currentRoll = 2;
                Invoke(nameof(PrepareSecondRoll), 3.5f);
            }
        }
        else
        {
            int frameTotal = firstRollPins + pinsKnocked;

            if (frameTotal >= 10)
            {
                Debug.Log($"SPARE! Jogador {currentPlayer}");
                AudioManager.Instance?.PlaySpare();
            }

            Invoke(nameof(EndPlayerTurn), 3.5f);
        }
    }

    void PrepareSecondRoll()
    {
        pinManager.RemoveFallenPins();
        powerUpZoneSpawner?.HidePowerUp();

        ResetBall();
        ballLauncher.SetCurrentPlayer(currentPlayer);

        Debug.Log($"Vez do Jogador {currentPlayer} — Frame {currentFrame} — Lançamento 2");
    }

    void EndPlayerTurn()
    {
        if (currentPlayer == 1)
            currentPlayer = 2;
        else
        {
            currentPlayer = 1;
            currentFrame++;
        }

        if (currentFrame > 10)
        {
            EndGame();
            return;
        }

        currentRoll = 1;
        firstRollPins = 0;

        pinManager.ResetPins();
        PrepareBall();

        ApplyPendingPowerUp();

        powerUpZoneSpawner?.RollSpawn();

        Debug.Log($"Vez do Jogador {currentPlayer} — Frame {currentFrame} — Lançamento 1");
        UIManager.Instance?.ShowTurnMessage(currentPlayer);
    }

    void PrepareBall()
    {
        ResetBall();
        ballLauncher.SetCurrentPlayer(currentPlayer);
    }

    void ApplyPendingPowerUp()
    {
        if (PowerUpManager.Instance == null) return;

        ResetBallPowerUpEffects();

        PowerUpType power = PowerUpManager.Instance.GetPowerUpToApplyOnPlayer(currentPlayer);

        if (power == PowerUpType.None) return;

        Debug.Log($"Power-up aplicado ao Jogador {currentPlayer}: {power}");

        switch (power)
        {
            case PowerUpType.SmallBall:
                ApplySmallBall();
                break;

            case PowerUpType.Mud:
                ApplyMud();
                break;

            case PowerUpType.Ice:
                ApplyIce();
                break;

            case PowerUpType.Obstacles:
                if (ObstacleManager.Instance != null)
                    ObstacleManager.Instance.SpawnObstacles();
                break;
        }
    }

    void ApplySmallBall()
    {
        ballObject.transform.localScale = originalBallScale * 0.6f;

        Rigidbody rb = ballObject.GetComponent<Rigidbody>();
        if (rb != null)
            rb.mass = originalBallMass * 0.8f;

        Debug.Log("Bola Pequena aplicada.");
        AudioManager.Instance?.PlaySmallBall();
    }

    void ApplyMud()
    {
        if (mudZone != null)
        {
            mudZone.SetActive(true);
            Debug.Log("Lama aplicada na pista.");
        }
    }

    void ApplyIce()
    {
        if (iceZone != null)
        {
            iceZone.SetActive(true);
            Debug.Log("Gelo aplicado na pista.");
        }
    }

    void ResetBallPowerUpEffects()
    {
        ballObject.transform.localScale = originalBallScale;

        Rigidbody rb = ballObject.GetComponent<Rigidbody>();
        if (rb != null)
            rb.mass = originalBallMass;

        if (mudZone != null)
            mudZone.SetActive(false);

        if (iceZone != null)
            iceZone.SetActive(false);

        if (ObstacleManager.Instance != null)
            ObstacleManager.Instance.ClearObstacles();
    }

    void ResetBall()
    {
        ballObject.transform.position = ballStartPosition;
        ballObject.transform.rotation = Quaternion.identity;

        Rigidbody rb = ballObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (CameraManager.Instance != null)
            CameraManager.Instance.ActivateMainCamera();
    }

    void EndGame()
    {
        gameEnded = true;

        int scoreP1 = GetScore(1);
        int scoreP2 = GetScore(2);

        if (ballLauncher != null)
            ballLauncher.enabled = false;

        powerUpZoneSpawner?.HidePowerUp();
        ResetBallPowerUpEffects();

        GameResult.SetResult(scoreP1, scoreP2);

        AudioManager.Instance?.PlayGameOver();
        Invoke(nameof(LoadEndScene), 1.5f);
    }

    void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    List<int> GetCurrentRolls()
    {
        return currentPlayer == 1 ? rollsP1 : rollsP2;
    }

    public int GetScore(int player)
    {
        List<int> rolls = player == 1 ? rollsP1 : rollsP2;
        return ScoreMaster.Score(rolls);
    }

    public int GetCurrentPlayer() => currentPlayer;
    public int GetCurrentFrame() => currentFrame;
    public int GetCurrentRoll() => currentRoll;
}