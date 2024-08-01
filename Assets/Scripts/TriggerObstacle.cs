using UnityEngine;

public class TriggerObstacle : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            playerController.animator.SetBool("Fall", true);
            PlayerController.fallTrue = true;
        }
    }
}
