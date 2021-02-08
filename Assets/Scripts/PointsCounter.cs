using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
    private Text text;
    private int id;
    private int points = 0;

    void Start()
    {
        text = GetComponent<Text>();
        id = Random.Range(0, 10000);
        text.text = ""+points;
    }

    public int GetId(){
        return id;
    }

    public void IncrementPoint(){
        points++;
        text.text = ""+points;
    }

    public int GetPoints(){
        return points;
    }

    public void GameOver(){
        GetComponent<Animator>().Play("Base Layer.Close");
    }
}
