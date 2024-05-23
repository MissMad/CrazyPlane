using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySocketIO;
using UnitySocketIO.Events;

public class SpawnControllableObjectsOverWS : MonoBehaviour
{
    public static SpawnControllableObjectsOverWS instance;
    public GameObject objectToSpawn;
    public GameManager gameManager;

    SocketIOController io;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        io = SocketIOController.instance;
        io.On("connect", (SocketIOEvent e) =>
        {
            Debug.Log("SocketIO connected");
        });

        io.Connect();

        // Ici on programme ce qui va se passer lorsqu'on va recevoir un message "spawn".
        // Ce message contient des donnée, entre autre le pseudo du joueur.
        // Tout le contenu du message va être mise dans la variable e ci-dessous.
        // Et pour accèder précisément au pseudo du joueur. il faudra utiliser cette syntaxe: e.data
        io.On("spawn", (SocketIOEvent e) =>
        {
            ////Lorsqu'on recoit un message "spawn".
            //// On verifie qu'il n'existe pas déjà un joueur du même nom.
            //if (GameManager.instance.spawnedObjects.Find(x => x.name == e.data) == null)
            //{
            //    //Si on ne trouve pas son nom dans la liste des joueurs instanciés,
            //    //C'est un nouveau joueur. On doit donc l'instancier.

            //    Debug.Log(e.data);// On affiche dans la console le pseudo joueur.

            //    // On instancie un prefab joueur.
            //    GameObject tmp = Instantiate(objectToSpawn,
            //                    new Vector3(Random.Range(-10, 10), Random.Range(-8, 8), 0),
            //                    Quaternion.identity);

            //    // On renome l'objet avec le pseudo du joueur.
            //    tmp.name = e.data;

            //    // On met à jour l'affichage de son pseudo. 
            //    if (tmp.transform.GetComponentInChildren<TMPro.TextMeshPro>())
            //    {
            //        tmp.transform.GetComponentInChildren<TMPro.TextMeshPro>().text = tmp.name;
            //    }

            //    // On ajouter le joueur à la liste des joueurs instanciés.
            //    GameManager.instance.spawnedObjects.Add(tmp);

            //    // Début de code pour générer une UI pour les scores.
            //    // Attention - ce code n'est pas finalisé.

            //    GameObject uiTmp = Instantiate(GameManager.instance.playerScorePrefab);
            //    uiTmp.transform.SetParent(GameManager.instance.scoreCanvas.transform);
            //    Debug.Log("nb players " + GameManager.instance.spawnedObjects.Count);
            //    uiTmp.GetComponent<RectTransform>().position = new Vector3((200 * GameManager.instance.spawnedObjects.Count), 1080 - 150, 0);
            //    uiTmp.transform.Find("Pseudo").GetComponent<UnityEngine.UI.Text>().text = tmp.name;
            //    tmp.GetComponent<ControlOverWS>().scoreDisplay = uiTmp;
            //    GameManager.instance.scoreList.Add(uiTmp.GetComponent<ScoreData>());

            //}
            //else// Si le joueur est déjà dans la liste, on ne l'instancie pas à nouveau.
            //{
            //    Debug.Log("User" + e.data + " already exist");
            //}

        });

    }

}
