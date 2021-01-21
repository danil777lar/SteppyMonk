using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMoves : MonoBehaviour
{
    public Transform leftFoot;
    public Transform rightFoot;

    private float standartFootHeight;
    private float standartHipsHeight;

    void Start(){
        standartFootHeight = leftFoot.position.y;
        standartHipsHeight = transform.position.y;
    }

    void Update()
    {
        HipsStabilization();
    }

    private void HipsStabilization(){
        float dif = Mathf.Min(leftFoot.position.y, rightFoot.position.y) - standartFootHeight;
        transform.position = new Vector3(transform.position.x, standartHipsHeight-dif, transform.position.z);
        
    }
}
