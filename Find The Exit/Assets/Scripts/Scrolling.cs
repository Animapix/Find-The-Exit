using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [SerializeField] public float speed = 4.0f;

    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
