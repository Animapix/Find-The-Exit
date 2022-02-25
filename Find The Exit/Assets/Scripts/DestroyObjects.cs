using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            other.GetComponentInParent<Tile>().DestroyTile(Random.Range(1f, 4f), Random.Range(1f, 5f));
        }else if (other.CompareTag("Props"))
        {
            Destroy(other.gameObject);
            
        }

    }
}
