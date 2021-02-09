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
        transform.localScale = new Vector3(.2f, .2f, .2f);
        choosed = true;
    }

    public void Restart(){
        // anim
        transform.localScale = new Vector3(3f, 3f, 3f);
        choosed = false;
        clicable = false;
    }
}
