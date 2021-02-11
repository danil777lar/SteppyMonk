using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Button playButton;
    public Button adButton;
    public Button backButton;
    public Button restartButton;

    public Chest[] chests;

    private bool firstTry = true;

    private int rewardId;

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
                    ShowChestClickedUI();
                    for (int i = 0; i < chests.Length; i++){
                        chests[i].clicable = false;
                    }
                }
            }
        }
    }

    private void ShowChestClickedUI(){
        SwitchUIElement(playButton, false); 
        SwitchUIElement(backButton, false);
        SwitchUIElement(adButton, true);
        SwitchUIElement(restartButton, true);

        if (!firstTry) adButton.interactable = false;
        firstTry = false;

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

        int rewardNum = Resources.LoadAll<Mask>("Objects/Masks/").Length;
        int[] ids = new int[chests.Length];

        for(int i = 0; i < chests.Length; i++){
            bool isCorrect = false;

            while(!isCorrect){
                isCorrect = true;
                int id = Random.Range(0, rewardNum);
                for(int j = 0; j < i; j++){
                    if (ids[j] == id) isCorrect = false;
                }
                ids[i] = id;
            }
            if (firstTry) chests[i].ShowChest(ids[i]);
            else chests[i].ShowChest();
            Debug.Log(""+ids[i]);
        }
        UpdateUI();
        Animator[] ui = GetComponentsInChildren<Animator>();
        for (int i = 0; i < ui.Length; i++){
            ui[i].Play("Base Layer.Close");
        }
    }
    
    public void RestartButtonClicked(){
        // GetReward
        bool isMoney = false;
        string inventory = PlayerPrefs.GetString("Inventory");
        string[] splitedInventory = inventory.Split(':');
        for (int i = 0; i < splitedInventory.Length; i++){
            if (int.Parse(splitedInventory[i]) == rewardId) isMoney = true;
        }

        if (isMoney) MoneyManager.EarnMoney(50);
        else {
            PlayerPrefs.SetString("Inventory", inventory+":"+rewardId);
            PlayerPrefs.Save();
        }

        // Restart all
        firstTry = true;
        for (int i = 0; i < chests.Length; i++){
            chests[i].Restart();
        }
        Animator[] ui = GetComponentsInChildren<Animator>();
        for (int i = 0; i < ui.Length; i++){
            ui[i].Play("Base Layer.Close");
        }
        Invoke("RestartInvoke", 1f);
    }

    private void RestartInvoke(){
        adButton.interactable = true;
        SwitchUIElement(playButton, true); 
        SwitchUIElement(backButton, true);
        SwitchUIElement(adButton, false);
        SwitchUIElement(restartButton, false);

        UpdateUI();

        Animator[] ui = GetComponentsInChildren<Animator>();
        for (int i = 0; i < ui.Length; i++){
            ui[i].transform.localScale = new Vector3(1f, 1f, 1f);
            ui[i].Play("Base Layer.Open");
        }
    }

    private void SwitchUIElement(Button btn, bool isActive){
        btn.transform.localScale = new Vector3(1f, 1f, 1f);
        btn.gameObject.SetActive(isActive);
    }

    public void AdButtonClicked(){
        //TODO - make ad
        Play();  
    }

    public void SetReward(int maskId){
        rewardId = maskId;
    }
}
