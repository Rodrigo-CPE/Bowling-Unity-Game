using UnityEngine;

public class PinZoneTrigger : MonoBehaviour
{
    private PinManager pinManager;

    void Start()
    {
        pinManager = GetComponent<PinManager>();
        if (pinManager == null)
            pinManager = FindFirstObjectByType<PinManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "bowlingball" || 
            other.gameObject.CompareTag("Ball"))
        {
            pinManager?.BallEnteredPinZone();
        }
    }
}