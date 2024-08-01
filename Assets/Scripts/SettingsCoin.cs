using UnityEngine;

public class SettingsCoin : MonoBehaviour
{
    public static int coin;
    public static bool isCoin;

    private void Start()
    {
        isCoin = false;

        coin = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coin++;
            isCoin = true;
            Destroy(other.gameObject);
        }
    }
}
