using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarComboManager : MonoBehaviour
{
    private List<Pillar> pillarList = new List<Pillar>();
    private List<Pillar> comboList = new List<Pillar>();

    private ComboCounter counter;
    private WalkManager walkManager;

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

    public int PillarStepped(Pillar pillar, ComboCounter counter, WalkManager walkManager){
        this.counter = counter;
        this.walkManager = walkManager;
        if (comboList.Count == 0) comboList.Add(pillar);
        else {
            if ( pillarList.IndexOf(pillar) == pillarList.IndexOf(comboList[comboList.Count-1])+1 ){
                comboList.Add(pillar);
                pillar.ComboAdd();
                lastStepTime = Time.time;                
            }
            else ClearComboList();
        }
        return pillarList.IndexOf(pillar);
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
        for (int i = 0; i < pillarList.Count; i++){
            pillarList[i].Restart();
        }
        ClearComboList();
        maxCombo = 1;
    }

    public void PillarCrushed(Pillar pillar){
        walkManager.PillarCrushed(pillarList.IndexOf(pillar));
    }
}
