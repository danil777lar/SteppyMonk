using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMoneyWidget : MonoBehaviour
{
    private Animator animator;
    private Text text;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
    }

    public void EarnMoney(int money){
        MoneyManager.EarnMoney(money);
        int total = PlayerPrefs.GetInt("Money");

        text.text = ""+total;
        animator.Play("Special Layer.Open");
    }
}
