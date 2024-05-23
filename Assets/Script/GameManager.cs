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
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI chrono;
    private float TimeNow = 0f;
    [SerializeField] public List<PlayerData> PlayerJoin = new List<PlayerData>();
    [SerializeField] private List<PlayerData> PlayerInGame = new List<PlayerData>();
    private bool Setup = false;
    private bool GameDecompte = false;
    private bool GamePlay = false;
    private bool GameQuiz = false;
    public bool GameReady = false;
    private bool Quiz1 = false;
    private bool scorePhaseExecuted = false; // Nouveau booléen pour suivre l'exécution de PhaseScore
    public SpawnLogique techspawn;
    [SerializeField] public PaperPlaneTest Plane;
    SocketIOController io;
    private System.Action<SocketIOEvent> input1Action, input2Action;
    [SerializeField] public Canvas ConnexionPage;
    [SerializeField] public TextMeshProUGUI NbJoinPlayer;
    [SerializeField] public Canvas ChargementPage;
    public GameObject smoke;
    private string serverUrl = "http://192.168.1.23:3000/gameState";
    private List<Canvas> newBullefirst = new List<Canvas>();
    private int etape = 0;

    void Start()
    {
        smoke.SetActive(false);
        ChargementPage.enabled = false;
        Setup = true;
        chrono.text = "0";
        TimeNow = 0f;
        io = SocketIOController.instance;

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
            string data = e.data.Trim('\\', '"');
            String[] param = data.Split('#');
            Debug.Log(data);
            String param4new = param[4].Trim('\\', '"', ' ');
            Debug.Log(param4new);
            techspawn.LastForce = float.Parse(param[4].Replace('.', ',')) * 15f;
            Debug.Log(techspawn.LastForce);
            techspawn.LastCommandeDirection = new Vector3(float.Parse(param[1]), float.Parse(param[2]), float.Parse(param[3]));
            techspawn.PaperCoord(e, techspawn.LastCommandeDirection, techspawn.LastForce);
        };
        io.On("swipe", input1Action);
        StartCoroutine(SendGameState());
    }

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

        if (GameQuiz && !scorePhaseExecuted) // Vérifiez si PhaseScore a déjà été exécuté
        {
            PhaseScore();
        }

        if (Quiz1)
        {
            Qcm1();
        }
    }

    private void Decompte()
    {
        smoke.SetActive(true);
        TimeNow += Time.deltaTime;
        chrono.text = "Time :" + Mathf.Round(TimeNow);

        // Détruire les anciennes bulles et vider la liste
        foreach (Canvas c in newBullefirst)
        {
            Destroy(c.gameObject);
        }
        newBullefirst.Clear();

        // Créer de nouvelles bulles
        foreach (PlayerData p in PlayerInGame)
        {
            p.CanShoot = false;
            Canvas newBulle = Instantiate(p.bulleequipe, p.Mains.transform.position, p.bulleequipe.transform.rotation);
            newBullefirst.Add(newBulle);
        }

        // Fin du décompte
        if (TimeNow > 5f)
        {
            TimeNow = 0;
            GameDecompte = false;
            GamePlay = true;

            StartCoroutine(SendGameState());
            foreach (Canvas c in newBullefirst)
            {
                Destroy(c.gameObject);
            }
            newBullefirst.Clear();
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
            scorePhaseExecuted = false; // Réinitialiser le booléen pour permettre l'exécution de PhaseScore
            StartCoroutine(SendGameState());
        }
    }

    private void PhaseScore()
    {
        scorePhaseExecuted = true; // Marquez PhaseScore comme exécuté
        TimeNow += Time.deltaTime;
        var tmpPlayers = PlayerInGame.ToList().OrderByDescending(el => el.planeInZone).ToList();
        int bestScore = tmpPlayers[0].planeInZone;
        bool playerEquality = tmpPlayers.Count(el => el.planeInZone == bestScore) > 1;

        foreach (PlayerData p in PlayerInGame)
        {
            p.CanShoot = false;
        }
        chrono.text = "Score TIME";
        if (PlayerInGame.Count > 0)
        {
            if (!playerEquality)
            {
                PlayerData firstPlayer = PlayerInGame.First(el => el.planeInZone == bestScore);
                Debug.Log("exclu" + firstPlayer.PlayerPseudo);
                Canvas newBulle = Instantiate(firstPlayer.bulleequipe, firstPlayer.Mains.transform.position, firstPlayer.bulleequipe.transform.rotation);
                newBullefirst.Add(newBulle);

                if (TimeNow > 5f)
                {
                    TimeNow = 0;
                    etape++;

                    StartCoroutine(SendGameState());
                    foreach (Canvas c in newBullefirst)
                    {
                        Destroy(c.gameObject);
                    }
                    newBullefirst.Clear();
                }
                firstPlayer.CanShoot = false;
                PlayerInGame.Remove(firstPlayer);

                if (etape == 1)
                {
                    Quiz1 = true;
                    GameDecompte = false;
                }
            }
            else if (playerEquality)
            {
                Debug.Log("Nouvelle Manche");
            }
        }

        // LOGIQUE DU PROF QUI POSE UNE QUESTION
    }

    private void setupGame()
    {
        if (GameReady)
        {
            // Rendre la page d'accueil visible
            NbJoinPlayer.text = "COMPLET";

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
                    StartCoroutine(SendGameState());
                }
            }
        }
        else
        {
            ConnexionPage.enabled = true;
            NbJoinPlayer.text = PlayerJoin.Count.ToString();
        }
    }

    IEnumerator SendGameState()
    {
        var gameState = new
        {
            GamePlay = this.GamePlay,
            Setup = this.Setup,
            GameQuiz = this.GameQuiz
        };

        string json = JsonUtility.ToJson(gameState);
        UnityWebRequest request = new UnityWebRequest(serverUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending game state: " + request.error);
        }
        else
        {
            Debug.Log("Game state sent successfully");
        }
    }

    private void Qcm1()
    {
        TimeNow += Time.deltaTime;
        Debug.Log("Quiz1");
        if (TimeNow > 5)
        {
            GamePlay = true;
            Quiz1 = false;
        }
    }
}
