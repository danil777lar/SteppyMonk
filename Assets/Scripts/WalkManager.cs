using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkManager : MonoBehaviour
{
    public Transform hips;
    public Transform leftFoot;
    public Transform rightFoot;

    public GameStateManager stateManager;

    private int currentLeg = 0;

    private Vector3 startPosition;
    private float startTime = -1f;

    private float deathPoint;
    
    void Update()
    {
        HipsToCenter();
        StepAnimation();  

        if (!EventSystem.current.IsPointerOverGameObject()){
            if (Input.GetMouseButtonDown(0) && startTime == -1f) StartStep();
            if (Input.GetMouseButtonUp(0) && startTime != -1f) EndStep();
        }      
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
        Vector3 rayOrigin = GetCurrentLeg().position;
        rayOrigin.y += 5f; 
        Ray ray = new Ray(rayOrigin, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit)){
            Debug.Log(hit);
            if (hit.transform.tag == "Opora") Debug.Log("OK");
            else if (hit.transform.tag == "Point") {
                hit.transform.gameObject.GetComponent<CheckPointGenerator>().Generate();
            } else GameOver();
        } else GameOver();
    }

    private void GameOver(){
        deathPoint = Mathf.Max(leftFoot.transform.position.x, rightFoot.transform.position.x);
        stateManager.ChangePanel(2);
    }

    public float GetDeathPoint(){
        return deathPoint;
    }

}
