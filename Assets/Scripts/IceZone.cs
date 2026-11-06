using UnityEngine;

public class IceZone : MonoBehaviour
{
    public float sideSlipForce = 3f;
    public float speedBoost = 1.08f;

    private bool affected = false;
    private bool hasPlayedSound = false;

    void OnEnable()
    {
        affected = false;
        hasPlayedSound = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        affected = true;

        rb.linearVelocity *= speedBoost;

        if (!hasPlayedSound)
        {
            AudioManager.Instance?.PlayIceSlide();
            VFXManager.Instance?.PlayMudVFX(other.transform.position);
            hasPlayedSound = true;
        }

        Debug.Log("A bola entrou no gelo.");
    }

    void OnTriggerStay(Collider other)
    {
        if (!affected) return;
        if (!other.CompareTag("Ball")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        float randomSlip = Random.Range(-sideSlipForce, sideSlipForce);
        rb.AddForce(Vector3.right * randomSlip, ForceMode.Force);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        affected = false;

        Debug.Log("A bola saiu do gelo.");
    }
}