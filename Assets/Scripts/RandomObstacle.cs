using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : MonoBehaviour
{
    public List<GameObject> obstacles;
    public float obstacleSpeed;
    public float spawnDistance = 10f; 
    private GameObject clone;
    public GameObject playerController;

    private void Update()
    {
        if (SettingsMenu.activeRunner && !PlayerController.fallTrue)
        {
            if (clone != null)
            {
                clone.transform.position += obstacleSpeed * Time.deltaTime * new Vector3(0, 0, -transform.forward.z);
            }
        }
    }

    public void SpawnObstacles()
    {
        // Önceki engeli yok et
        if (clone != null)
        {
            Destroy(clone);
        }

        // Yeni engel oluştur
        GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];
        float spawnZ = playerController.transform.position.z + spawnDistance;
        clone = Instantiate(obstacle, new Vector3(0, 0, spawnZ), obstacle.transform.rotation);
    }
}
