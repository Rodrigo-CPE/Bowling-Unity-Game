using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [Header("Configurações")]
    public float launchForce = 18f;
    public float aimSpeed = 60f;
    public float maxAimAngle = 30f;
    private BallSpin ballSpin;
    private float selectedSpin = 0f;

    private Rigidbody rb;
    private LineRenderer lr;
    private float aimAngle = 0f;
    private bool launched = false;
    private int currentPlayer = 1;

    void Start()
    {
        ballSpin = GetComponent<BallSpin>();
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.positionCount = 2;
            lr.startWidth = 0.06f;
            lr.endWidth = 0.01f;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = Color.yellow;
            lr.endColor = new Color(1f, 0.4f, 0f);
        }
    }

    public void SetCurrentPlayer(int player)
    {
        currentPlayer = player;
        launched = false;
        aimAngle = 0f;
        transform.rotation = Quaternion.identity;
        if (lr != null) lr.enabled = true;

        selectedSpin = 0f;

        if (ballSpin != null)
            ballSpin.ResetSpin();
    }

    void Update()
    {
        if (!launched)
        {
            HandleAiming();
            HandleSpinInput();
            DrawAimLine();
            HandleLaunch();
        }
    }

    void HandleAiming()
    {
        if (currentPlayer == 1)
        {
            if (Input.GetKey(KeyCode.A)) aimAngle -= aimSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.D)) aimAngle += aimSpeed * Time.deltaTime;
        }
        else if (currentPlayer == 2)
        {
            if (Input.GetKey(KeyCode.J)) aimAngle -= aimSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.L)) aimAngle += aimSpeed * Time.deltaTime;
        }

        aimAngle = Mathf.Clamp(aimAngle, -maxAimAngle, maxAimAngle);
        transform.rotation = Quaternion.Euler(0, aimAngle, 0);
    }

    void HandleLaunch()
    {
        bool launchPressed = false;

        if (currentPlayer == 1 && Input.GetKeyDown(KeyCode.Space))
            launchPressed = true;
        if (currentPlayer == 2 && Input.GetKeyDown(KeyCode.Return))
            launchPressed = true;

        if (launchPressed)
        {
            launched = true;

            if (lr != null)
                lr.enabled = false;

            // Mudar automaticamente para a Ball Camera
            if (CameraManager.Instance != null)
                CameraManager.Instance.ActivateBallCamera();


            Vector3 direction =
                Quaternion.Euler(0, aimAngle, 0) * Vector3.forward;

            rb.AddForce(direction * launchForce, ForceMode.Impulse);
            AudioManager.Instance?.PlayBallRoll();

            if (ballSpin != null)
                ballSpin.SetSpin(selectedSpin);
        }
    }

    void HandleSpinInput()
    {
        selectedSpin = 0f;

        if (Input.GetKey(KeyCode.Q))
            selectedSpin = -1f;

        if (Input.GetKey(KeyCode.E))
            selectedSpin = 1f;
    }

    void DrawAimLine()
    {
        if (lr == null) return;
        Vector3 dir = Quaternion.Euler(0, aimAngle, 0) * Vector3.forward;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + dir * 5f);
    }
}