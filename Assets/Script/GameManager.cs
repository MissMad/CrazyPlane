using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnitySocketIO.Events;
using UnitySocketIO;
using UnityEditor.Experimental.GraphView;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI chrono;
    private float TimeNow = 0f;
    [SerializeField] public  List<PlayerData> PlayerJoin = new List<PlayerData>();
    [SerializeField] private List<PlayerData> PlayerInGame = new List<PlayerData>();
    private bool Setup = false;
    private bool GameDecompte = false;
    private bool GamePlay = false;
    private bool GameQuiz = false;
    public bool GameReady = false;
    public SpawnLogique techspawn;
    [SerializeField] public PaperPlaneTest Plane;
    SocketIOController io;
    private System.Action<SocketIOEvent> input1Action, input2Action;
    [SerializeField] public Canvas ConnexionPage;
    [SerializeField] public TextMeshProUGUI NbJoinPlayer;
    [SerializeField] public Canvas ChargementPage;
    public GameObject smoke;
    // Start is called before the first frame update
    void Start()
    {
        smoke.SetActive(false);
        ChargementPage.enabled = false;
        Setup = true;
        chrono.text = "0";
        TimeNow = 0f;
        io = SocketIOController.instance;
        //GameDecompte = true; //TEST//////////////////////

        io.On("connect", (SocketIOEvent e) =>
        {
            Debug.Log("SocketIO connected");
        });

        io.Connect();

        io.On("spawn", (SocketIOEvent e) =>
        {
            PlayerData PConcerne = PlayerJoin[0];
            string data = e.data.Trim('"');
            string[] param = data.Split('#');
            PConcerne.PlayerPseudo = data;
            PlayerJoin.Remove(PConcerne);
            PlayerInGame.Add(PConcerne);
            Debug.Log("PlayerAdd");
        });


        input1Action = (SocketIOEvent e) =>
        {
            string data = e.data.Trim('\\','"');
            String[] param = data.Split('#');
            Debug.Log(data);
            String param4new = param[4].Trim('\\', '"', ' ');
            Debug.Log(param4new);
            techspawn.LastForce = float.Parse(param[4].Replace('.',','));
            Debug.Log(techspawn.LastForce);
            techspawn.LastCommandeDirection = new Vector3(float.Parse(param[1]), float.Parse(param[2]), float.Parse(param[3]));
            techspawn.PaperCoord(e, techspawn.LastCommandeDirection , techspawn.LastForce);
        };
        io.On("swipe", input1Action);

    }

    // Update is called once per framea
    void Update()
    {
        if (Setup)
        {
            setupGame();
        }
        if (PlayerInGame.Count == 8)
        {
            GameReady = true;
        }

        if (GameDecompte)
        {
            Decompte();
            
        }

        if (GamePlay)
        {
            PhaseGameOne();
        }

        if (GameQuiz)
        {
            PhaseScore();
        }

       }

    private void Decompte()
    {

        smoke.SetActive(true);
        TimeNow += Time.deltaTime;
        chrono.text = ("Time :" + Mathf.Round(TimeNow));
        foreach (PlayerData p in PlayerInGame)
        {
            p.CanShoot = false;
            
        }
        if (TimeNow > 5f)
        {
            TimeNow = 0;
            GameDecompte = false;
            GamePlay = true;
        }
    }

    private void PhaseGameOne()
    {
        foreach (PlayerData p in PlayerInGame)
        {
            p.CanShoot = true;
        }
        chrono.text = "PLAY";

        TimeNow += Time.deltaTime;
        if (TimeNow > 15f)
        {
            TimeNow = 0;
            GameDecompte = false;
            GameQuiz = true;
        }
    }

    private void PhaseScore()
    {
        var tmpPlayers = PlayerInGame.ToList().OrderByDescending(el => el.planeInZone).ToList();
        int bestScore = tmpPlayers[0].planeInZone;
        bool playerEquality = tmpPlayers.Count(el => el.planeInZone == bestScore) > 1;

        foreach (PlayerData p in PlayerJoin)
        {
            p.CanShoot = false;
        }
        chrono.text = "Score TIME";
        
        if (!playerEquality)
        {
            PlayerData firstPlayer = PlayerInGame.First(el => el.planeInZone == bestScore);
            Debug.Log("exclu" + firstPlayer.PlayerPseudo);
        }
        else
        {
            Debug.Log("Nouvelle Manche");
        }
       



        //LOGIQUE DU PROF QUI POSE UNE QUESTION
    }

    private void setupGame()
    {
        if(GameReady)
        {
            //Rendre la page acceuil visible
            
            NbJoinPlayer.text = ("COMPLET");

            TimeNow += Time.deltaTime;
            if (TimeNow > 5)
            {
                ChargementPage.enabled = true;
                ConnexionPage.enabled = false;
                if (TimeNow > 12) 
                {
                    ChargementPage.enabled = false;
                    GameDecompte = true;
                    GameReady = false;
                    TimeNow = 0;
                    Setup = false;
                }
            }
        }
        else
        {
            ConnexionPage.enabled = true;
            NbJoinPlayer.text = PlayerJoin.Count.ToString();

        }


    }

}
