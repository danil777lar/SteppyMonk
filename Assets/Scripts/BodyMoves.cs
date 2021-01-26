using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMoves : MonoBehaviour
{
    private WalkManager walkManager;

    private float legLenght = 5f;
    private float normalPoint;

    void Awake(){
        walkManager = GetComponent<WalkManager>();
        normalPoint = transform.position.y;
    }

    void Update()
    {
        HipsStabilization();
    }

    private void HipsStabilization(){
        Vector3 newPos = transform.position;
        float max = Mathf.Max(walkManager.leftFoot.position.x, walkManager.rightFoot.position.x);
        float min = Mathf.Min(walkManager.leftFoot.position.x, walkManager.rightFoot.position.x); 
        newPos.y = normalPoint - ((max-min)/4f); 

        transform.position = newPos;
    }
}
