using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] float duration = 1.5f;
    [SerializeField] Transform firePoint;
    [SerializeField] LayerMask laserImpactLayerMask;
    float elapsedTime = 0;
    bool isActive = false;
    public System.Action<float> ConsumeEnergyCallback;
    [SerializeField] public float energyToShoot = 1.0f;

    private Tile target = null;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, laserImpactLayerMask))
        {
            GetComponent<RobotController>().currentState = RobotController.State.Fire;
            isActive = true;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);
            target = hit.collider.GetComponentInParent<Tile>();
        }
    }

    private void Update()
    {
        if (isActive)
        {
            elapsedTime += Time.deltaTime;
            ConsumeEnergyCallback(energyToShoot * Time.deltaTime);
            if (elapsedTime >= duration)
            {
                lineRenderer.enabled = false;
                elapsedTime = 0;
                isActive = false;
                GetComponent<RobotController>().currentState = RobotController.State.Idle;
                target.isWall = false;
                target = null;
            }
        }
        
    }
}
