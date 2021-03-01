using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPanel : Panel, IAdCreator
{
    public GameObject player;

    private InterstitialAdGenerator[] ad;

    private bool firstAd;

    private void Start(){
        base.Start();
        ad = GetComponentsInChildren<InterstitialAdGenerator>();
    }

    public override void Close(){
        base.Close();
    }

    public override void Open()
    {
        GenerateCounter();
        base.Open();
    }

    public void RestartButtonClicked(){
        if (Random.Range(0, 3) != 0) Restart();
        else {
            firstAd = true;
            ad[0].Show(this);
        }
    }

    private void Restart(){
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

    public void RewAdClosed(){}

    public void IntAdClosed(){
        if (firstAd){
            firstAd = false;
            ad[1].Show(this);
        } else Restart();
    }
}
