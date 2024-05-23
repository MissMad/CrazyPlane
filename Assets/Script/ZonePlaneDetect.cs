using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZonePlaneDetect : MonoBehaviour
{
    [SerializeField] int PlaneIn = 0;
    public PlayerData player = null;
    [SerializeField] public List<GameObject> listAvions = new List<GameObject>();

    private void Update()
    {
        player.planeInZone = PlaneIn;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "plane")
        {
            PlaneIn++;
            listAvions.Add(other.gameObject);
        }
    }
}
