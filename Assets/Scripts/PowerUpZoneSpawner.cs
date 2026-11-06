using UnityEngine;

public class PowerUpZoneSpawner : MonoBehaviour
{
    [Header("Referência")]
    public GameObject powerUpZone;

    [Header("Probabilidade")]
    [Range(0f, 1f)]
    public float spawnChance = 0.45f;

    [Header("Posições possíveis")]
    public Transform[] spawnPoints;

    public void RollSpawn()
    {
        if (powerUpZone == null || spawnPoints.Length == 0)
            return;

        powerUpZone.SetActive(false);

        float roll = Random.value;

        if (roll > spawnChance)
        {
            Debug.Log("Power-up não apareceu nesta jogada.");
            return;
        }

        int index = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[index];

        powerUpZone.transform.position = chosenPoint.position;
        powerUpZone.transform.rotation = chosenPoint.rotation;
        powerUpZone.SetActive(true);

        PowerUpZone zone = powerUpZone.GetComponent<PowerUpZone>();
        if (zone != null)
            zone.ResetZone();

        Debug.Log("Power-up apareceu em: " + chosenPoint.name);
    }

    public void HidePowerUp()
    {
        if (powerUpZone != null)
            powerUpZone.SetActive(false);
    }
}