using UnityEngine;

[RequireComponent(typeof(RobotMovement))]
public class RobotController : MonoBehaviour
{
    RobotMovement robotMovement;
    Laser laser;

    [SerializeField] float energy = 200;
    [SerializeField] EnergyBarController energyUI;

    private float maxEnergy = 100;

    public enum State
    {
        Idle,
        Move,
        Fire
    }

    public State currentState = State.Idle;

    public (int column, int row) coordinates { 
        get {
            int column = Mathf.FloorToInt(transform.position.x / robotMovement.stepSize);
            int row = Mathf.FloorToInt(transform.position.z / robotMovement.stepSize);
            return (column, row);
        } 
    }

    private void Awake()
    {
        robotMovement = GetComponent<RobotMovement>();
        robotMovement.ConsumeEnergyCallback = ConsumeEnergy;
        maxEnergy = energy;

        laser = GetComponent<Laser>();
        laser.ConsumeEnergyCallback = ConsumeEnergy;
    }

    private void Start()
    {
        energyUI.SetMax(energy);
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && currentState == State.Idle)
        {
            laser.Fire();
        }
    }

    public void ConsumeEnergy(float amount)
    {
        energy -= amount;
        energyUI.SetValue(energy);
        if (energy < 0)
        {
            energy = 0;
            robotMovement.timeToMove = 1.0f;
        }
    }

    public void AddEnergy(float amount)
    {
        energy += amount;
        energyUI.SetValue(energy);
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }

}
