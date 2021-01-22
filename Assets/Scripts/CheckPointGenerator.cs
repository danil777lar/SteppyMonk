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
        if (startGenerate) Generate();
    }

    public void Generate(){
        if (isNewPoint){
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
