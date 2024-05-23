//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class PlayerScore
//{
//    public int score = 0;
//    public string name;
//    public int id;

//    public PlayerScore(int id, string name)
//    {
//        this.id = id;
//        this.name = name;
//    }
//}

//public class ScoreManager : MonoBehaviour
//{
//    public List<PlayerScore> scoreList;
//    public List<PlayerScore> sortedScoreList;
//    public Canvas scoreSlot;

//    public int scoreSlotCount = 5;
//    // Start is called before the first frame update
//    void Start()
//    {
//        scoreList = new List<PlayerScore>();
//        sortedScoreList = new List<PlayerScore>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        scoreList.Sort();
//        foreach (var score in scoreList)
//        {

//        }
//    }

//    void sortList()
//    {
//        sortedScoreList = scoreList;

//    //    int max = 0;
//    //    for (int i = scoreList.Count - 1; i > 1; i++)
//    //    {
//    //        if (scoreList[i].score > scoreList[i - 1].score)
//    //        {

//    //        }
//    //    }
//    //}

////}
