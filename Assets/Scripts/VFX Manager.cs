using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }

    [Header("VFX Prefabs")]
    public GameObject strikeVFX;
    public GameObject iceVFX;
    public GameObject mudVFX;
    public GameObject obstacleHitVFX;

    [Header("Positions")]
    public Transform strikeSpawnPoint;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayStrikeVFX()
    {
        if (strikeVFX == null || strikeSpawnPoint == null) return;

        Instantiate(strikeVFX, strikeSpawnPoint.position, Quaternion.identity);
    }

    public void PlayIceVFX(Vector3 position)
    {
        if (iceVFX == null) return;

        Instantiate(iceVFX, position, Quaternion.identity);
    }

    public void PlayMudVFX(Vector3 position)
    {
        if (mudVFX == null) return;

        Instantiate(mudVFX, position, Quaternion.identity);
    }

    public void PlayObstacleHitVFX(Vector3 position)
    {
        if (obstacleHitVFX == null) return;

        Instantiate(obstacleHitVFX, position, Quaternion.identity);
    }
}