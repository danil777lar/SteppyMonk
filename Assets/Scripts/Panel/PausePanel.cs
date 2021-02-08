using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : Panel
{
    public override void Close(){
        base.Close();
        Time.timeScale = 1f;
    }

    public override void Open()
    {
        base.Open();
        Invoke("InvokeOpen", 1f);
    }

    private void InvokeOpen(){
        Time.timeScale = 0f;
    }
}
