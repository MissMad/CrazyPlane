using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    public Transform rotationPivot;
    public float rotationSpeed;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotationPivot.position, Vector3.forward, (rotationSpeed * Time.deltaTime) + offset);
    }
}
