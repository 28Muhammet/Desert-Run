using UnityEngine;

public class SpawnerObstacle : MonoBehaviour
{
    public GameObject obstacleManager;
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSpawned)
        {
            obstacleManager.GetComponent<RandomObstacle>().SpawnObstacles();
            hasSpawned = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hasSpawned = false; 
    }
}
