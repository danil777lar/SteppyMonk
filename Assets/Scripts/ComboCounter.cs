using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{
    private Animator animator;
    private Text text;

    private int value = 1;

    private void Start(){
        text = GetComponent<Text>();
        text.text = "x" + value;
        animator = GetComponent<Animator>();
    }

    public void UpdateValue(int newValue){
        value = newValue;
        Close();
        Invoke("UpdateUI", .5f);
    }

    private void UpdateUI(){
        text.text = "x" + value;
        Open();
    }

    public int PullValue(){
        int v = value;
        UpdateValue(1);
        return v;
    }

    public void Close(){
        animator.Play("Base Layer.Close");
    }

    public void Open(){
        animator.Play("Base Layer.Open");
    }
}
