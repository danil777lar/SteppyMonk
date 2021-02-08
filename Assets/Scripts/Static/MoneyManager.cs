using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyManager
{
    public static void EarnMoney(int money){
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+money);
        PlayerPrefs.Save();
    }
}
