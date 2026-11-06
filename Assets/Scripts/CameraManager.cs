using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [Header("Cameras")]
    public Camera mainCamera;
    public Camera ballCamera;
    public Camera pinCamera;

    [Header("Targets")]
    public Transform ballTarget;

    [Header("Ball Camera")]
    public float followDistance = 4f;
    public float followHeight = 1.4f;
    public float lookAhead = 2f;
    public float followSmoothness = 8f;

    [Header("Auto Switch")]
    public float pinCameraSwitchZ = 11.5f;

    private int currentCameraIndex = 0;
    private bool switchedToPinCamera = false;
    private Vector3 lastBallDirection = Vector3.forward;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ActivateMainCamera();

    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
            CycleCamera();

        FollowBallCamera();
        CheckPinCameraSwitch();
    }

    public void ActivateMainCamera()
    {
        switchedToPinCamera = false;
        SetCamera(0);
    }

    public void ActivateBallCamera()
    {
        switchedToPinCamera = false;
        SetCamera(1);
        SnapBallCameraBehindBall();
    }

    public void ActivatePinCamera()
    {
        SetCamera(2);
    }

    void CycleCamera()
    {
        currentCameraIndex++;

        if (currentCameraIndex > 2)
            currentCameraIndex = 0;

        SetCamera(currentCameraIndex);
    }

    void SetCamera(int index)
    {
        currentCameraIndex = index;

        if (mainCamera != null)
            mainCamera.gameObject.SetActive(index == 0);

        if (ballCamera != null)
            ballCamera.gameObject.SetActive(index == 1);

        if (pinCamera != null)
            pinCamera.gameObject.SetActive(index == 2);
    }

    void SnapBallCameraBehindBall()
    {
        if (ballCamera == null || ballTarget == null) return;

        Vector3 desiredPosition = GetBallCameraPosition();

        ballCamera.transform.position = desiredPosition;
        ballCamera.transform.LookAt(GetLookTarget());
    }

    void FollowBallCamera()
    {
        if (ballCamera == null || ballTarget == null) return;
        if (!ballCamera.gameObject.activeSelf) return;

        Vector3 desiredPosition = GetBallCameraPosition();

        ballCamera.transform.position = Vector3.Lerp(
            ballCamera.transform.position,
            desiredPosition,
            followSmoothness * Time.deltaTime
        );

        ballCamera.transform.LookAt(GetLookTarget());
    }

    Vector3 GetBallCameraPosition()
    {
        Vector3 direction = GetBallDirection();

        return ballTarget.position
             - direction * followDistance
             + Vector3.up * followHeight;
    }

    Vector3 GetLookTarget()
    {
        Vector3 direction = GetBallDirection();

        return ballTarget.position
             + direction * lookAhead
             + Vector3.up * 0.3f;
    }

    Vector3 GetBallDirection()
    {
        Rigidbody rb = ballTarget.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = 0;

            if (velocity.magnitude > 0.2f)
                lastBallDirection = velocity.normalized;
        }

        return lastBallDirection;
    }

    void CheckPinCameraSwitch()
    {
        if (ballTarget == null) return;
        if (switchedToPinCamera) return;

        if (ballTarget.position.z >= pinCameraSwitchZ)
        {
            ActivatePinCamera();
            switchedToPinCamera = true;
        }
    }
}