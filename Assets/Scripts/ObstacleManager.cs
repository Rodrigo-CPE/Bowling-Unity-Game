using UnityEngine;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance { get; private set; }

    [Header("Referências")]
    public GameObject obstaclePrefab;

    private List<GameObject> activeObstacles = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public void SpawnObstacles()
    {
        ClearObstacles();

        CreateObstacle(
            new Vector3(-0.35f, 0.2f, 8.5f),
            Quaternion.Euler(0f, 15f, 0f)
        );

        CreateObstacle(
            new Vector3(0.45f, 0.2f, 11f),
            Quaternion.Euler(0f, -10f, 0f)
        );

        Debug.Log("Obstáculos aplicados na pista.");
    }

    void CreateObstacle(Vector3 position, Quaternion rotation)
    {
        if (obstaclePrefab == null)
        {
            Debug.LogWarning("Obstacle prefab não está atribuído.");
            return;
        }

        GameObject obstacle = Instantiate(obstaclePrefab, position, rotation);
        activeObstacles.Add(obstacle);
    }

    public void ClearObstacles()
    {
        foreach (GameObject obstacle in activeObstacles)
        {
            if (obstacle != null)
                Destroy(obstacle);
        }

        activeObstacles.Clear();
    }
}