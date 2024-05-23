using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySocketIO;
using UnitySocketIO.Events;

public class SpawnOverWebSocket : MonoBehaviour
{
    public GameObject objectToSpawn;

    SocketIOController io;
    // Start is called before the first frame update
    void Start()
    {
        io = SocketIOController.instance;
        io.On("connect", (SocketIOEvent e) => {
            Debug.Log(e);
            Debug.Log("SocketIO connected");
        });

        io.Connect();

        io.On("spawn", (SocketIOEvent e) => {
            Debug.Log(e.data);
            Instantiate(    objectToSpawn, 
                            new Vector3( Random.Range(-10, 10),Random.Range(-8, 8),0),
                            Quaternion.identity).gameObject.name = e.data;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
