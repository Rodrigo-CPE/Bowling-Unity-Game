using UnityEngine;

public class Pin : MonoBehaviour
{
    public float standingThreshold = 60f;
    public float minHitVelocity = 1.5f;

    private Rigidbody rb;
    private bool hasPlayedHitSound = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsStanding()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);
        return angle < standingThreshold;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasPlayedHitSound) return;

        if (collision.gameObject.CompareTag("Ball"))
        {
            if (collision.relativeVelocity.magnitude >= minHitVelocity)
            {
                AudioManager.Instance?.PlayPinHit();

                if (collision.contacts.Length > 0)
                {
                    Vector3 hitPoint = collision.contacts[0].point + Vector3.up * 0.25f;
                    VFXManager.Instance?.PlayObstacleHitVFX(hitPoint);
                }

                hasPlayedHitSound = true;
            }
        }
    }
}