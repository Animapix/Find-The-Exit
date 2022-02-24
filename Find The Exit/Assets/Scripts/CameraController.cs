using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothFactor = 0.125f;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothFactor);
        transform.position = smoothPosition;
        transform.LookAt(smoothPosition);
    }



}
