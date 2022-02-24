using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{

    [SerializeField] float energy = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<RobotController>().AddEnergy(energy);
            Destroy(gameObject);
        }
        
    }
}
