using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject cam; 
    public GameObject player;

    private Panel[] panels;
    private int state = 0;
    
    private void Start()
    {
        panels = GetComponentsInChildren<Panel>();
        ChangePanel(0);    
    }

    public void ChangePanel(int newState){
        panels[state].Close();
        panels[newState].Open();
        state = newState;
    }
}
