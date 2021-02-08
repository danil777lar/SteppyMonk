using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPanel : Panel
{
    public GameObject player;

    public override void Close(){
        base.Close();
    }

    public override void Open()
    {
        GenerateCounter();
        base.Open();
    }

    public void Restart(){
        float x = player.GetComponentInChildren<WalkManager>().GetSpawn();
        Destroy(player);
        player = Instantiate(Resources.Load<GameObject>("Objects/PlayerDir"));
        player.transform.position = new Vector3(x, -1.770314f, 3.14077f);
        player.GetComponentInChildren<WalkManager>().stateManager = GetComponentInParent<GameStateManager>();
    }

    private void GenerateCounter(){
        Text[] texts = GetComponentsInChildren<Text>();
        int points = player.GetComponentInChildren<PointsCounter>().GetPoints();
        int maxPoints = PlayerPrefs.GetInt("Max_Points");

        if (points > maxPoints){
            maxPoints = points;
            PlayerPrefs.SetInt("Max_Points", maxPoints);
            PlayerPrefs.Save();
        }

        texts[0].text = ""+points;
        texts[1].text = "best\n"+maxPoints;
        texts[2].text = ""+PlayerPrefs.GetInt("Money");

    }
}
