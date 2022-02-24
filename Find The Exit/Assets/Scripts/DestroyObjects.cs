using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            Destroy(other.transform.parent.gameObject);
        }else if (other.CompareTag("Props"))
        {
            Destroy(other.gameObject);
        }

    }
}
