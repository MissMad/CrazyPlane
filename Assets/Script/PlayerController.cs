using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool CanShoot = true;
    public bool TestShoot = false;
    public Vector3 Direction = new Vector3(0, 0, 0);
    public GameObject plane;
    const string Tir = "Tir";

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(PlayerData player, GameObject objectSpawn)
    {
        Animator animator = player.PlayerSkin.GetComponent<Animator>();
        if (animator != null)
        {
            Debug.Log("Animator component found.");
            if (animator.HasState(0, Animator.StringToHash(Tir)))
            {
                Debug.Log("Animation state 'Tir' found in the Animator.");
                animator.Play(Tir);
            }
            else
            {
                Debug.LogError("Animation state 'Tir' not found in the Animator.");
            }
        }
        else
        {
            Debug.LogError("Animator component not found on PlayerSkin.");
        }

        Debug.Log("Shoot");
        Debug.Log("INFO" + this.transform.position);
        GameObject newPlane = Instantiate(plane,
                              player.Mains.transform.position,
                              Quaternion.identity);
        PaperPlaneTest techDirectionChange = newPlane.GetComponentInChildren<PaperPlaneTest>();
        techDirectionChange.Controllerdirection(player.directionplane, player.ForceEnvoi);
    }
}
