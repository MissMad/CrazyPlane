using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnitySocketIO;
//using UnitySocketIO.Events;

public class ControlOverWS : MonoBehaviour
{

    //SocketIOController io;

    //Rigidbody rgbd;

    //public float power = 6;
    //public Transform directionMarker;
    //public float directionMarkerInitLength = 0.1f;
    //public float directionMarkerMaxLength = 2;
    //public float directionMarkerGrowSpeed = 0.1f;
    //public float directionMarkerCurrentLength;
    //public float inactifDelay = 10;
    //public bool isActif = true;
    //private float timer = 0;
    //private Vector3 direction;
    //private Vector3 initialSize;
    //public int score = 0;
    //public int scoreIncr = 1;
    //public float scoreTickDelay = 2;
    //public float scoreTickTimer = 0;
    //public GameObject scoreDisplay;

    //private System.Action<SocketIOEvent> input1Action, input2Action;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    initialSize = transform.localScale;
    //    directionMarkerCurrentLength = directionMarkerInitLength;
    //    rgbd = GetComponent<Rigidbody>();

    //    io = SocketIOController.instance;

    //    input1Action = (SocketIOEvent e) => {

    //        if (e.data == gameObject.name)
    //        {

    //            Debug.Log("UP");
    //            if (directionMarker)
    //            {
    //                direction = directionMarker.position - transform.position;
    //            }
    //            else
    //            {
    //                direction = Vector3.up;
    //            }
    //            rgbd.AddForce(direction * power, ForceMode.Impulse);
    //            isActif = true;
    //            timer = 0;
    //        }

    //    };

    //    input2Action = (SocketIOEvent e) => {
    //        if (e.data == gameObject.name)
    //        {
    //            if (directionMarker)
    //            {
    //                directionMarker.GetComponent<TurnAround>().rotationSpeed *= -1;
    //                isActif = true;
    //                timer = 0;
    //            }
    //        }
    //    };

    //    io.On("input1", input1Action);

    //    io.On("input2", input2Action);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (timer > inactifDelay)
    //    {
    //        isActif = false;
    //    }

    //    if (isActif)
    //    {
    //        resetLength();
    //    }
    //    else
    //    {
    //        growUp();
    //    }

    //    scoreTickTimer += Time.deltaTime;
    //    if (scoreTickTimer > scoreTickDelay)
    //    {
    //        updateScore();
    //        scoreTickTimer = 0;
    //    }
    //}

    //private void updateScore()
    //{
    //    score += scoreIncr;
    //    if (scoreDisplay)
    //        scoreDisplay.GetComponent<ScoreData>().updateDisplay(score);
    //}

    //private void growUp()
    //{
    //    if (directionMarkerCurrentLength < directionMarkerMaxLength)
    //    {
    //        directionMarkerCurrentLength += directionMarkerGrowSpeed * Time.deltaTime;
    //    }
    //    directionMarker.localScale = new Vector3(directionMarkerCurrentLength, initialSize.y, initialSize.z);

    //}

    //private void resetLength()
    //{
    //    directionMarkerCurrentLength = directionMarkerInitLength;
    //    directionMarker.localScale = new Vector3(directionMarkerCurrentLength, initialSize.y, initialSize.z);
    //}

    //private void OnDestroy()
    //{
    //    io.Off("input1", input1Action);
    //    io.Off("input2", input2Action);
    //    Destroy(scoreDisplay);
    //    GameManager.instance.scoreList.Remove(scoreDisplay.GetComponent<ScoreData>());

    //}
}
