using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : Panel
{
    public override void Close(){
        base.Close();
        GetComponentInParent<GameStateManager>().cam.GetComponent<Animator>().Play("Base Layer.ToInventory");
    }

    public override void Open()
    {
        base.Open();
        GetComponentInParent<GameStateManager>().cam.GetComponent<Animator>().Play("Base Layer.ToShop");
    }
}
