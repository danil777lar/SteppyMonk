using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Shop shop;

    public bool choosed = false;
    public bool clicable = false;

    public void ConnectShop(Shop sh){
        shop = sh;
    }

    public void ShowChest(){
        // anim
        clicable = true;
    }

    public void ChooseChest(){
        // anim
        choosed = true;
    }

    void Start()
    {
        
    }
}
