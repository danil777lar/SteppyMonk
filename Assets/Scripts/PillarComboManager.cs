using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarComboManager : MonoBehaviour
{
    private List<Pillar> pillarList = new List<Pillar>();
    private List<Pillar> comboList = new List<Pillar>();

    private ComboCounter counter;

    private float lastStepTime = -1f;
    private float comboDuration = 1.2f; 

    private int maxCombo = 1;

    private void Update(){
        if ( lastStepTime != -1f && Time.time - lastStepTime > comboDuration ){
            ClearComboList();
        }
    }

    public void SetPillarList(List<GameObject> pillarObjectList){
        for (int i = 0; i < pillarObjectList.Count; i++){
            pillarList.Add( pillarObjectList[i].GetComponent<Pillar>() );
        }
    }

    public void PillarStepped(Pillar pillar, ComboCounter counter){
        this.counter = counter;
        if (comboList.Count == 0) comboList.Add(pillar);
        else {
            if ( pillarList.IndexOf(pillar) == pillarList.IndexOf(comboList[comboList.Count-1])+1 ){
                comboList.Add(pillar);
                pillar.ComboAdd();
                lastStepTime = Time.time;                
            }
            else ClearComboList();
        }
    }

    private void ClearComboList(){
        lastStepTime = -1f;
        if (comboList.Count-1 > maxCombo){
            maxCombo = comboList.Count-1;
            counter.UpdateValue(maxCombo);
            for (int i = 1; i < comboList.Count; i++){
                comboList[i].ComboComplete();
            }
        } else {
            for (int i = 1; i < comboList.Count; i++){
                comboList[i].ComboLose();
            }
        }
        
        
        comboList.Clear();
    }

    public void RestartPillars(){
        ClearComboList();
        maxCombo = 1;
    }
}
