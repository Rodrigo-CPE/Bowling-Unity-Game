using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance?.PlayMenuMusic();
    }

    public void PlayGame()
    {
        AudioManager.Instance?.PlayButtonClick();
        AudioManager.Instance?.PlayGameMusic();

        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        AudioManager.Instance?.PlayButtonClick();

        Debug.Log("Jogo fechado");
        Application.Quit();
    }
}