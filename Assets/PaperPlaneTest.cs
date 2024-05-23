using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlaneTest : MonoBehaviour
{
    [Header("Direction Vectors")]
    public Vector3 direction1;
    [Header("Throw Force")]
    public float force;

    private Rigidbody rb;

    void Start()
    {
        
        
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on the object.");
        }

        ThrowPlane();
    }

    public void ThrowPlane()
    {
        if (rb != null)
        {
            Vector3 finalDirection = direction1.normalized;

            rb.AddForce(finalDirection * force, ForceMode.Impulse);
        }
    }

    public void Controllerdirection(Vector3 directioncontroller, float forceenvoi)
    {
        force = forceenvoi;
        direction1 = directioncontroller;
    }

}
