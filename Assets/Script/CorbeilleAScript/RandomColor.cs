using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField]
    public Gradient colors;
    [SerializeField]
    public bool autoDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        if (autoDestroy)
        {
            Destroy(gameObject, 60);
        }
        Random.InitState((int)(Time.timeSinceLevelLoad * 100));
        GetComponent<Renderer>().material.color = colors.Evaluate(Random.value);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
