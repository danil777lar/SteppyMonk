using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject cam; 
    public GameObject player;

    private Panel[] panels;
    private static int state = 0;
    private int newState = 0;
    
    private void Start()
    {
        state = 0;
        panels = GetComponentsInChildren<Panel>();
        ChangePanel(0);    
    }

    public void ChangePanel(int newState){
        this.newState = newState;
        panels[state].Close();
        Invoke("InvokeChange", 1f);
    }

    private void InvokeChange(){
        panels[newState].Open();
        state = newState;
    }

    public static int GetState(){
        return state;
    }
}
