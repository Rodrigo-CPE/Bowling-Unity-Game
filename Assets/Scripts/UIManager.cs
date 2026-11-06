using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Referências UI")]
    public TextMeshProUGUI playerTurnText;
    public TextMeshProUGUI frameText;
    public TextMeshProUGUI scoreP1Text;
    public TextMeshProUGUI scoreP2Text;


    private Coroutine turnMessageCoroutine;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (playerTurnText != null)
            playerTurnText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        if (frameText != null)
        {
            frameText.text = "Frame: " + GameManager.Instance.GetCurrentFrame()
                           + " / 10 | Lançamento: "
                           + GameManager.Instance.GetCurrentRoll();
        }

        if (scoreP1Text != null)
            scoreP1Text.text = "P1: " + GameManager.Instance.GetScore(1);

        if (scoreP2Text != null)
            scoreP2Text.text = "P2: " + GameManager.Instance.GetScore(2);
    }

    public void ShowTurnMessage(int player)
    {
        if (playerTurnText == null) return;

        if (turnMessageCoroutine != null)
            StopCoroutine(turnMessageCoroutine);

        turnMessageCoroutine = StartCoroutine(TurnMessageRoutine(player));
    }

    IEnumerator TurnMessageRoutine(int player)
    {
        playerTurnText.gameObject.SetActive(true);
        playerTurnText.text = "Vez do Jogador " + player;

        yield return new WaitForSeconds(2f);

        playerTurnText.gameObject.SetActive(false);
    }
}