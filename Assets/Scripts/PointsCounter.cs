using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
    private Animator animator;
    private Text text;
    private int id;
    private int points = 0;

    void Start()
    {
        text = GetComponent<Text>();
        id = Random.Range(0, 10000);
        text.text = ""+points;
        animator = GetComponent<Animator>();
    }

    public int GetId(){
        return id;
    }

    public void IncrementPoint(){
        points++;
        Close();
        Invoke("UpdateUI", .5f);
    }

    private void UpdateUI(){
        text.text = ""+points;
        Open();
    }

    public int GetPoints(){
        return points;
    }

    public void Close(){
        animator.Play("Base Layer.Close");
    }

    public void Open(){
        animator.Play("Base Layer.Open");
    }
}
