using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyManager
{
    public static void EarnMoney(int money){
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+money);
        PlayerPrefs.Save();
    }

    public static string GetMoneyText(){
        int money = PlayerPrefs.GetInt("Money");
        string text = ""+money;
        if (money > 999) text = "999+";
        return text;
    }

    public static bool SpendMoney(int cost){
        int money = PlayerPrefs.GetInt("Money");
        if (cost > money) return false;

        PlayerPrefs.SetInt("Money", money - cost);
        PlayerPrefs.Save();
        return true;
    }

    public static bool CheckMoney(int cost){
        int money = PlayerPrefs.GetInt("Money");
        if (cost > money) return false;
        else return true;
    }
}
