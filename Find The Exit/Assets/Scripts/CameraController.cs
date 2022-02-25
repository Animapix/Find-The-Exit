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



        transform.eulerAngles = new Vector3(Map(smoothPosition.x, 34, 82, 64, 54), transform.eulerAngles.y, transform.eulerAngles.z);
        offset = new Vector3(Map(smoothPosition.x, 34, 82, 50, 25), offset.y, offset.z);

    }


    private float Map(float input, float inputMin, float inputMax, float min, float max)
    {
        return min + (input - inputMin) * (max - min) / (inputMax - inputMin);
    }
}
