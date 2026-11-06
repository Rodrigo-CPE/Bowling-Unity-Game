using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndSceneManager : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        AudioManager.Instance?.PlayEndMusic();

        titleText.text = "FIM DE JOGO";

        if (GameResult.winner == 1)
            winnerText.text = "Vencedor: Jogador 1";
        else if (GameResult.winner == 2)
            winnerText.text = "Vencedor: Jogador 2";
        else
            winnerText.text = "Empate!";

        scoreText.text =
            "Jogador 1: " + GameResult.scoreP1 +
            "\nJogador 2: " + GameResult.scoreP2;
    }

    public void GoToMainMenu()
    {
        AudioManager.Instance?.PlayButtonClick();
        AudioManager.Instance?.PlayMenuMusic();

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        AudioManager.Instance?.PlayButtonClick();

        Debug.Log("Sair do jogo");
        Application.Quit();
    }
}