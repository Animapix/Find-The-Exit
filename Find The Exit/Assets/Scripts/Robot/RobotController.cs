using UnityEngine;

[RequireComponent(typeof(RobotMovement))]
public class RobotController : MonoBehaviour
{
    RobotMovement robotMovement;

    [SerializeField] float energy = 200;
    [SerializeField] EnergyBarController energyUI;

    private float maxEnergy = 100;

    private void Awake()
    {
        robotMovement = GetComponent<RobotMovement>();
        robotMovement.ConsumeEnergyCallback = ConsumeEnergy;
        maxEnergy = energy;
    }

    private void Start()
    {
        energyUI.SetMax(energy);
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
