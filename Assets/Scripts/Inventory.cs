﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public MaskLoader maskLoader;
    public Text title;
    public Text description;

    public Button leftButton;
    public Button rightButton;
    public Button okButton;

    private int currentMask;
    private int maskNum;

    void Start()
    {
        currentMask = PlayerPrefs.GetInt("Mask");
        maskNum = Resources.LoadAll<Mask>("Objects/Masks/").Length;
        maskLoader.LoadMask(currentMask);
        UpdateUI();
    }

    private void UpdateUI(){
        Mask mask = maskLoader.GetMask();
        title.text = mask.title;
        description.text = mask.description;

        if (currentMask == 0) leftButton.interactable = false;
        else leftButton.interactable = true;

        if (currentMask == maskNum-1) rightButton.interactable = false;
        else rightButton.interactable = true;

        if (currentMask == PlayerPrefs.GetInt("Mask")) okButton.interactable = false;
        else okButton.interactable = true;

        if (CheckIdInInventory(currentMask)) okButton.GetComponentInChildren<Text>().text = "OK";
        else okButton.GetComponentInChildren<Text>().text = "X";
    }

    public void GetNext(){
        currentMask++;
        maskLoader.LoadMask(currentMask);
        UpdateUI();
    }

    public void GetPrevious(){
        currentMask--;
        maskLoader.LoadMask(currentMask);
        UpdateUI();
    }

    public void OkButtonPressed(){
        if (CheckIdInInventory(currentMask)){
            PlayerPrefs.SetInt("Mask", currentMask);
            PlayerPrefs.Save();
        }
        UpdateUI();
    }

    public void BackButtonPressed(){
        currentMask = PlayerPrefs.GetInt("Mask");
        maskLoader.LoadMask(currentMask);
        UpdateUI();

        Invoke("BackInvoke", 1f);
    }

    private void BackInvoke(){
        GetComponentsInParent<Transform>()[1].gameObject.GetComponentInChildren<SceneTransition>().SwitchScene("SampleScene");
    }

    private bool CheckIdInInventory(int id){
        string[] masksInInventory = PlayerPrefs.GetString("Inventory").Split(':');
        for (int i = 0; i < masksInInventory.Length; i++){
            if (id == int.Parse(masksInInventory[i])) return true;
        }
        return false;
    }

}