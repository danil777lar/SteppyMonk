using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointGenerator : MonoBehaviour
{
    public bool startGenerate = false;

    private CheckPointGenerator creator;
    private bool isNewPoint = true;

    void Start()
    {
        GenerateMyself();
        if (startGenerate) GenerateNext();
    }

    private void GenerateMyself(){
        // oporas generate
        int workingSpace = 90;
        int oporaMaxWidth = 4;
        int stepMaxLenght = 10;

        int seed_oporasNum = Random.seed;
        int oporasNum = Random.Range(workingSpace/stepMaxLenght, workingSpace/oporaMaxWidth);

        Transform bottom = GetComponentsInParent<Transform>()[1];
        GameObject[] oporas = new GameObject[oporasNum];
        int oporaId = Random.Range(0, 1);
        for (int i = 0; i < oporasNum; i++){
            oporas[i] = Instantiate(Resources.LoadAll<GameObject>("Objects/Oporas/")[oporaId]);
            oporas[i].transform.parent = bottom;
            float xpos = -35f + (workingSpace/oporasNum)*i;
            oporas[i].transform.localPosition = new Vector3(xpos, -3f, 28f);
        } 
    }

    public void GenerateNext(){
        if (isNewPoint){
            GetComponentInChildren<Tree>().OpenLeaves();
            GameObject nextPoint = Instantiate(Resources.Load<GameObject>("Objects/CheckPoint"));
            nextPoint.transform.parent = transform.parent;
            Vector3 newPosition = transform.position;
            newPosition.x += 100f;
            nextPoint.transform.position = newPosition;
            isNewPoint = false;
            nextPoint.GetComponent<CheckPointGenerator>().SetCreator(this);
            if (creator != null) creator.DeletePrevious();
        }   
    }

    public void SetCreator(CheckPointGenerator c){
        if (creator == null) creator = c;
    }

    public void DeletePrevious(){
        Destroy(creator.gameObject);
    }
}
