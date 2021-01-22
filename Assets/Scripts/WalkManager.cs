using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkManager : MonoBehaviour
{
    public Transform hips;
    public Transform leftFoot;
    public Transform rightFoot;

    private int currentLeg = 0;

    private Vector3 startPosition;
    private float startTime = -1f;
    
    void Start()
    {
        
    }

    void Update()
    {
        HipsToCenter();
        StepAnimation();  

        if (Input.GetMouseButtonDown(0) && startTime == -1f) StartStep();
        if (Input.GetMouseButtonUp(0) && startTime != -1f) EndStep();      
    }

    private void HipsToCenter(){
        float leftx = leftFoot.position.x;
        float rightx = rightFoot.position.x;
        float center = 0f;

        if (leftx < rightx) hips.position = new Vector3( leftx+((rightx-leftx)/2), hips.position.y, hips.position.z );
        else hips.position = new Vector3( rightx+((leftx-rightx)/2), hips.position.y, hips.position.z );
    }

    private void StartStep(){
        ChangeLeg();
        startTime = Time.time;
        startPosition = GetCurrentLeg().position;
    }

    private void EndStep(){
        startTime = -1f;
        GetCurrentLeg().position = new Vector3(GetCurrentLeg().position.x, startPosition.y, GetCurrentLeg().position.z);
        CheckOpora();
    }

    private void StepAnimation(){
        if (startTime != -1f){
            Vector3 endPosition = GetOtherLeg().position;
            endPosition.x += 10f;
            endPosition.z = GetCurrentLeg().position.z;
            float duration = 1f;
            float t = (Time.time - startTime)/duration;
            GetCurrentLeg().position = Vector3.Lerp(startPosition, endPosition, t);

            float xPer = (t-0.5f)*2f;
            float y = startPosition.y + ( -(xPer*xPer)+2f );
            GetCurrentLeg().position = new Vector3(GetCurrentLeg().position.x, y, GetCurrentLeg().position.z);

            if (t > 1) EndStep();
        }
    }

    private Transform GetCurrentLeg(){
        if (currentLeg == 0) return leftFoot;
        else return rightFoot;
    }

    private Transform GetOtherLeg(){
        if (currentLeg == 1) return leftFoot;
            else return rightFoot;
    }

    private void ChangeLeg(){
        if (currentLeg == 0) currentLeg = 1;
        else currentLeg = 0;
    }

    private void CheckOpora(){
        Ray ray = new Ray(GetCurrentLeg().position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit)){
            if (hit.transform.tag == "Opora") Debug.Log("OK");
            else if (hit.transform.tag == "Point") {
                hit.transform.gameObject.GetComponent<CheckPointGenerator>().Generate();
            } else Debug.Log("LOX");
        } else Debug.Log("LOX");
    }

}
