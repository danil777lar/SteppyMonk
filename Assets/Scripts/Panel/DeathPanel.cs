using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : Panel
{
    public GameObject player;

    public override void Close(){
        base.Close();
    }

    public override void Open()
    {
        base.Open();
    }

    public void Restart(){
        float x = player.GetComponentInChildren<WalkManager>().GetDeathPoint() + 100f;
        x = ((x-(x%100)) - 100f)-5f;
        Destroy(player);
        player = Instantiate(Resources.Load<GameObject>("Objects/PlayerDir"));
        player.transform.position = new Vector3(x, -1.770314f, 3.14077f);
        player.GetComponentInChildren<WalkManager>().stateManager = GetComponentInParent<GameStateManager>();
    }
}
