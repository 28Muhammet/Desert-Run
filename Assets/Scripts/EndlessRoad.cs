using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
    public GameObject platform;
    public float platformSpeed;
    private float platformLength;

    public static bool globalSpeed = false;

    private void Start()
    {
        platformLength = platform.GetComponentInChildren<Renderer>().bounds.size.z;
    }

    private void Update()
    {
        if (SettingsMenu.activeRunner == true && PlayerController.fallTrue == false)
        {
            platform.transform.position += platformSpeed * Time.deltaTime * Vector3.back;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position += new Vector3(0, 0, platformLength * 3);
            globalSpeed = true;
        }
    }
}