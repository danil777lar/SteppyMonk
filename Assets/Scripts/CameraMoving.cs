using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform target;
    public float Offset_Y = 0f;
    public float Offset_X = 0f;
    public float Offset_Time = 0f;

    private Vector3 finish;

    void Update()
    {   
        if (!Input.GetMouseButton(0)) finish = new Vector3(target.position.x+Offset_X, target.position.y+Offset_Y, transform.position.z);
        float t = Time.deltaTime/Offset_Time;    
        transform.position = Vector3.Lerp(transform.position, finish, t);
    }
}
