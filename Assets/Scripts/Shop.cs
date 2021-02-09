using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Button playButton;
    public Button adButton;

    public Chest[] chests;

    private bool firstTry = true;

    void Start()
    {
        UpdateUI();
        ConnectChests();
    }

    void Update(){
        if (Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                Chest chest = hit.transform.gameObject.GetComponent<Chest>();
                if(chest != null && chest.clicable && !chest.choosed){
                    chest.ChooseChest();
                    chest.transform.localScale = new Vector3(.2f, .2f, .2f);
                    for (int i = 0; i < chests.Length; i++){
                        chests[i].clicable = false;
                        ShowChestClickedUI();
                        firstTry = false;
                    }
                }
            }
        }
    }

    private void ShowChestClickedUI(){
        playButton.gameObject.SetActive(false);
        adButton.gameObject.SetActive(true);

        Animator[] ui = GetComponentsInChildren<Animator>();
        for (int i = 0; i < ui.Length; i++){
            ui[i].Play("Base Layer.Open");
        }
    }

    private void ConnectChests(){
        for (int i = 0; i < chests.Length; i++){
            chests[i].ConnectShop(this);
        }
    }

    private void UpdateUI(){
        if (MoneyManager.CheckMoney(100)) playButton.interactable = true;
        else playButton.interactable = false;

        GetComponentInChildren<Text>().text = MoneyManager.GetMoneyText();
    }


    public void Play(){
        if (firstTry) MoneyManager.SpendMoney(100);
        for(int i = 0; i < chests.Length; i++){
            chests[i].ShowChest();
        }
        UpdateUI();
        Animator[] ui = GetComponentsInChildren<Animator>();
        for (int i = 0; i < ui.Length; i++){
            ui[i].Play("Base Layer.Close");
        }
    }


    public void AdButtonClicked(){
        //TODO - make ad
        Play();  
    }
}
