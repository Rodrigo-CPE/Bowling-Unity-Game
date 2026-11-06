using UnityEngine;

public class ObstacleHitSound : MonoBehaviour
{
    public float minHitVelocity = 0.2f;
    public float cooldown = 0.15f;

    private float lastPlayTime = -10f;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        if (Time.time - lastPlayTime < cooldown) return;

        if (collision.relativeVelocity.magnitude >= minHitVelocity)
        {
            AudioManager.Instance?.PlayObstacleHit();

            if (collision.contacts.Length > 0)
            {
                VFXManager.Instance?.PlayObstacleHitVFX(
                    collision.contacts[0].point
                );
            }

            lastPlayTime = Time.time;
        }
    }
}