using UnityEngine;

public class MudZone : MonoBehaviour
{
    public float slowMultiplier = 0.45f;

    private bool hasPlayedSound = false;

    void OnEnable()
    {
        hasPlayedSound = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        rb.linearVelocity *= slowMultiplier;
        rb.angularVelocity *= slowMultiplier;

        if (!hasPlayedSound)
        {
            AudioManager.Instance?.PlayMudSplash();
            VFXManager.Instance?.PlayMudVFX(other.transform.position);
            hasPlayedSound = true;
        }

        Debug.Log("A bola passou pela lama e perdeu velocidade.");
    }
}