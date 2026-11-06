using UnityEngine;
 
public class PinManager : MonoBehaviour
{
    [Header("Referências")]
    public GameObject pinPrefab;
 
    [Header("Configurações")]
    public Vector3 deckPosition = new Vector3(0, 0.3f, 14f);
    public float pinSpacing = 0.35f;
    public Vector3 pinRotationEuler = Vector3.zero;
 
    private GameObject[] pins = new GameObject[10];
 
    private Vector3[] pinFormation = new Vector3[]
    {
        new Vector3(0, 0, 0),
        new Vector3(-0.5f, 0, 0.9f),
        new Vector3( 0.5f, 0, 0.9f),
        new Vector3(-1f,   0, 1.8f),
        new Vector3( 0,    0, 1.8f),
        new Vector3( 1f,   0, 1.8f),
        new Vector3(-1.5f, 0, 2.7f),
        new Vector3(-0.5f, 0, 2.7f),
        new Vector3( 0.5f, 0, 2.7f),
        new Vector3( 1.5f, 0, 2.7f),
    };
 
    private float lastChangeTime;
    private int lastStandingCount = -1;
    private int lastSettledCount = 10;
 
    public bool ballInPinZone = false;
    private bool hasSettled = false;
 
    void Start()
    {
        SpawnPins();
    }
 
    void Update()
    {
        if (!ballInPinZone) return;
 
        int standing = CountStanding();
 
        if (standing != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = standing;
            return;
        }
 
        if (Time.time - lastChangeTime > 2f && lastStandingCount >= 0)
        {
            PinsHaveSettled();
        }
    }
 
    public void SpawnPins()
    {
        foreach (var pin in pins)
        {
            if (pin != null)
                Destroy(pin);
        }
 
        lastSettledCount = 10;
        lastStandingCount = -1;
        ballInPinZone = false;
        hasSettled = false;
 
        Quaternion spawnRotation = Quaternion.Euler(pinRotationEuler);
 
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = deckPosition + pinFormation[i] * pinSpacing;
            pins[i] = Instantiate(pinPrefab, pos, spawnRotation);
 
            Rigidbody rb = pins[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.Sleep();
            }
        }
    }
 
    public void ResetPins()
    {
        SpawnPins();
    }
 
    public void BallEnteredPinZone()
    {
        if (hasSettled) return;
        ballInPinZone = true;
        lastChangeTime = Time.time;
        lastStandingCount = CountStanding();
    }
 
    void PinsHaveSettled()
    {
        if (hasSettled) return;
        hasSettled = true;
 
        int standing = CountStanding();
        int fallen = lastSettledCount - standing;
 
        fallen = Mathf.Clamp(fallen, 0, 10);
 
        lastSettledCount = standing;
        lastStandingCount = -1;
        ballInPinZone = false;
 
        Debug.Log($"Pinos assentaram. Caídos: {fallen}, Em pé: {standing}");
 
        GameManager.Instance?.Bowl(fallen);
    }
 
    int CountStanding()
    {
        int standing = 0;
 
        foreach (var pin in pins)
        {
            if (pin == null) continue;
 
            Pin p = pin.GetComponent<Pin>();
            if (p != null && p.IsStanding())
                standing++;
        }
 
        return standing;
    }
 
    public void RemoveFallenPins()
    {
        int standingAfterRemoval = 0;
        for (int i = 0; i < pins.Length; i++)
        {
            if (pins[i] == null) continue;
 
            Pin p = pins[i].GetComponent<Pin>();
 
            if (p != null && !p.IsStanding())
            {
                Destroy(pins[i]);
                pins[i] = null;
            }
            else
            {
                standingAfterRemoval++;
            }
        }
 
        lastSettledCount = standingAfterRemoval;
        lastStandingCount = -1;
        ballInPinZone = false;
        hasSettled = false;
        Debug.Log("Pinos caídos removidos. Pinos restantes: " + standingAfterRemoval);
    }
}