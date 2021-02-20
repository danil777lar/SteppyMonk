using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointGenerator : MonoBehaviour
{
    // Links
    [SerializeField]
    private Transform pillarsRoot;
    
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
        Vector3 startPoint = new Vector3(50.01f, -10.81f, 28.1f);
        float workingSpace = 90f;
        int pillareMax = 5;
        int pillarMin = 2;
        int stepMax = 8;
        int stepMin = 2;

        ArrayList pillarList = new ArrayList();
        int currentPoint = 0;
        while (currentPoint < workingSpace){
            currentPoint += Random.Range(stepMin, stepMax);
            if (workingSpace - currentPoint > pillareMax){
                int pillarLenght = Random.Range(pillarMin, pillareMax+1);
                GameObject pillar = Instantiate(Resources.Load<GameObject>("Objects/Pillars/Simple/pillar_"+pillarLenght));
                pillarList.Add(pillar);
                pillar.transform.parent = pillarsRoot.transform;
                pillar.transform.localScale = new Vector3(1f, 1f, 1f);
                pillar.transform.localPosition = new Vector3( startPoint.x - currentPoint, startPoint.y, startPoint.z );

                currentPoint += pillarLenght;
            } else break;
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
            if (!startGenerate) GetComponent<AudioSource>().Play();
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
