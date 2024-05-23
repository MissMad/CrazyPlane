using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData : MonoBehaviour
{
    public int score;
    public int id;
    public int pos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateDisplay(int score)
    {
        this.score = score;
        transform.Find("Score").GetComponent<UnityEngine.UI.Text>().text = score.ToString();
    }


}
