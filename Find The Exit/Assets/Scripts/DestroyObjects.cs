using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {

            float delay = Map(GetComponent<Scrolling>().speed, 2.0f, 5.0f, 5.0f, 2.0f);


            other.GetComponentInParent<Tile>().DestroyTile(Random.Range(1f, 4f), Random.Range(0f, delay));
        }else if (other.CompareTag("Props"))
        {
            Destroy(other.gameObject);
            
        }

        if (other.CompareTag("Player"))
        {
            GameController.instance.GameOver();
        }
    }


    private float Map(float input, float inputMin, float inputMax, float min, float max)
    {
        return min + (input - inputMin) * (max - min) / (inputMax - inputMin);
    }
}
