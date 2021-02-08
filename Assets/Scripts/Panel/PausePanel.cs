using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : Panel
{
    public override void Close(){
        base.Close();
        Time.timeScale = 1f;
    }

    public override void Open()
    {
        base.Open();
        GetComponentInChildren<Text>().text = ""+PlayerPrefs.GetInt("Money");
        Invoke("InvokeOpen", 1f);
    }

    private void InvokeOpen(){
        Time.timeScale = 0f;
    }
}
