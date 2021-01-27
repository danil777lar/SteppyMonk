using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Transform leaves;
    private float startTime = -1f;
    private bool closed = true;

    private float duration = 1f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(-90f, Random.Range(0f, 180f), 0f);
        leaves = GetComponentsInChildren<Transform>()[1];
    }

    private void Update(){
        if (startTime != -1f){
            float t = (Time.time-startTime)/duration;
            Debug.Log(""+t);
            leaves.localScale = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f), t);
            if(t >= 1f) startTime = -1f;
        }
    }

    public void OpenLeaves(){
        if (closed){
            startTime = Time.time;
            closed = false;
        } 
    }
}
