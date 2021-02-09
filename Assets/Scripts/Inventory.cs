using System.Collections;
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

    void Awake()
    {
        currentMask = PlayerPrefs.GetInt("mask");
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

        if (CheckIdInInventory(currentMask)) okButton.interactable = true;
        else okButton.interactable = false;
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
        PlayerPrefs.SetInt("Mask", currentMask);
    }

    private bool CheckIdInInventory(int id){
        string[] masksInInventory = PlayerPrefs.GetString("Inventory").Split(':');
        for (int i = 0; i < masksInInventory.Length; i++){
            if (id == int.Parse(masksInInventory[i])) return true;
        }
        return false;
    }

}
