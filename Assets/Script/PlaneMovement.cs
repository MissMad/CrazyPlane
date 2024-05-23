using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float speed = 6;

    void Update()
    {
        // Moves the object forward at two units per second.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
