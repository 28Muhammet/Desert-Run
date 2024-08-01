using UnityEngine;

public class SettingsCamera : MonoBehaviour
{
    public Transform character; 
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 

    private void LateUpdate()
    {
        Vector3 desiredPosition = character.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
