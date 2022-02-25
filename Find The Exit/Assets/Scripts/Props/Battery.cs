using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{

    [SerializeField] float energy = 15;
    [SerializeField] int scoreAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.instance.AddToScore(scoreAmount);
            other.GetComponent<RobotController>().AddEnergy(energy);
            Destroy(gameObject);
        }
        
    }
}
