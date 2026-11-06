using UnityEngine;

public class BallSpin : MonoBehaviour
{
    [Header("Configuração do Spin")]
    public float spinSideForce = 8f;
    public float maxSpinTime = 4f;

    private Rigidbody rb;
    private float activeSpin = 0f;
    private float spinTimer = 0f;
    private bool spinActive = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetSpin(float spinDirection)
    {
        activeSpin = Mathf.Clamp(spinDirection, -1f, 1f);
        spinTimer = maxSpinTime;
        spinActive = Mathf.Abs(activeSpin) > 0.01f;
    }

    public void ResetSpin()
    {
        activeSpin = 0f;
        spinTimer = 0f;
        spinActive = false;
    }

    void FixedUpdate()
    {
        if (!spinActive || rb == null) return;

        if (rb.linearVelocity.magnitude < 0.5f)
            return;

        Vector3 sideDirection = Vector3.right * activeSpin;

        rb.AddForce(sideDirection * spinSideForce, ForceMode.Force);

        spinTimer -= Time.fixedDeltaTime;

        if (spinTimer <= 0f)
            ResetSpin();
    }
}