using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointGenerator : MonoBehaviour
{
    // Links
    public Transform pillars;
    
    // Values
    public bool startGenerate = false;


    // Links
    private CheckPointGenerator creator;
    private Text text;

    // Values
    private bool isNewPoint = true;
    private int moduleLenght = 10;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        GenerateMyself();
        if (startGenerate) GenerateNext();
    }

    private void GenerateMyself(){
        // Pillars generate
        float workingSpace = 90f;
        int oporaMaxWidth = 4;
        int stepMaxLenght = 10;

        int seed_oporasNum = Random.seed;

        int workingSpaceInt = Mathf.FloorToInt(workingSpace);
        int oporasNum = Random.Range(workingSpaceInt/stepMaxLenght, workingSpaceInt/oporaMaxWidth);

        GameObject[] oporas = new GameObject[oporasNum];
        int oporaId = Random.Range(0, 1);
        for (int i = 0; i < oporasNum; i++){
            oporas[i] = Instantiate(Resources.LoadAll<GameObject>("Objects/Oporas/")[oporaId]);
            oporas[i].transform.parent = pillars;
            float xpos = -40f + (workingSpace/oporasNum)*i;
            oporas[i].transform.localPosition = new Vector3(xpos, -3f, 28f);

            if (xpos > 43f) Destroy(oporas[i]);
        } 
    }

    public bool GenerateNext(){
        if (isNewPoint){
            GameObject nextPoint = Instantiate(Resources.Load<GameObject>("Objects/CheckPoint"));
            nextPoint.transform.parent = transform.parent;
            Vector3 newPosition = transform.position;
            newPosition.x += 100f;
            nextPoint.transform.position = newPosition;
            isNewPoint = false;
            nextPoint.GetComponentInChildren<CheckPointGenerator>().SetCreator(this);
            if (creator != null && creator.creator != null) creator.DeletePrevious();

            // Show Myself Text
            if (!startGenerate) {
                PlayerPrefs.SetInt( "Distance_0", PlayerPrefs.GetInt("Distance_0") + moduleLenght );
                PlayerPrefs.Save();
            }
            text.text = "" + PlayerPrefs.GetInt( "Distance_0" ) + "m";
            GetComponent<Animator>().Play("Base Layer.OpenDistanceText");
            return true;
        }   
        return false;
    }

    public void SetCreator(CheckPointGenerator c){
        if (creator == null) creator = c;
    }

    public void DeletePrevious(){
        Destroy(creator.gameObject);
    }
}
