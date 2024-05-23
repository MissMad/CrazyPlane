using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "PlayerData", order = 1)]

public class PlayerData : ScriptableObject
{
    public string PlayerPseudo;
    public GameObject PlayerSkin;
    public bool CanShoot = false;
    [SerializeField] public int planeInZone = 0;
    [SerializeField] public GameObject Mains;
    public ZonePlaneDetect ZonePlayer = null;
    [SerializeField] public float ForceEnvoi;
    public Vector3 directionplane;
    public GameObject placement;
    public string Equipe;
   
}
