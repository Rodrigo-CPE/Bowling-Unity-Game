using UnityEngine;

public class PowerUpZone : MonoBehaviour
{
    private bool alreadyCollected = false;

    void OnTriggerEnter(Collider other)
    {
        if (alreadyCollected) return;

        if (other.CompareTag("Ball"))
        {
            alreadyCollected = true;

            int player = GameManager.Instance.GetCurrentPlayer();
            PowerUpManager.Instance.CollectRandomPowerUp(player);

            AudioManager.Instance?.PlayPowerUpPickup();

            Debug.Log("Power-up recolhido pelo Jogador " + player);

            gameObject.SetActive(false);
        }
    }

    public void ResetZone()
    {
        alreadyCollected = false;
        gameObject.SetActive(true);
    }
}