using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkManager : MonoBehaviour
{
    public Transform hips;
    public Foot leftFoot;
    public Foot rightFoot;

    public GameStateManager stateManager;
    public Camera cam;
    public PointsCounter pointCounter;
    public ComboCounter comboCounter;

    public float duration = 5f;

    private Material glovingMaterial;
    private Material normalMaterial;

    private bool isWalkable = true;
    private int currentLeg = 0;

    private Vector3 startPosition;
    private float startTime = -1f;

    private float spawnPoint;

    private Pillar pillar;

    private int[] lastPillarId = {-1, -1};

    void Start(){
        rightFoot.StartAnim(1);
        spawnPoint = GetComponentInParent<Transform>().GetComponentInParent<Transform>().position.x;
    }

    public void SetWalkable(bool isWalkable){
        this.isWalkable = isWalkable;
    }
    
    void Update()
    {
        HipsToCenter();
        StepAnimation();  

        if (GameStateManager.GetState() == 0 && isWalkable){
            if (Input.GetMouseButtonDown(0) && startTime == -1f) StartStep();
            if (Input.GetMouseButtonUp(0) && startTime != -1f) EndStep();
        }      
    }

    private void HipsToCenter(){
        float leftx = leftFoot.transform.position.x;
        float rightx = rightFoot.transform.position.x;
        float center = 0f;

        if (leftx < rightx) hips.position = new Vector3( leftx+((rightx-leftx)/2), hips.position.y, hips.position.z );
        else hips.position = new Vector3( rightx+((leftx-rightx)/2), hips.position.y, hips.position.z );
    }

    private void StartStep(){
        lastPillarId[0] = -1;        
        ChangeLeg();
        startTime = Time.time;
        startPosition = GetCurrentLeg().position;
    }

    private void EndStep(){
        startTime = -1f;
        GetCurrentLeg().position = new Vector3(GetCurrentLeg().position.x, startPosition.y, GetCurrentLeg().position.z);
        if(CheckOpora()){
            if (currentLeg == 0) {
                leftFoot.StartAnim(0);
                rightFoot.StartAnim(1);
            } else {
                leftFoot.StartAnim(1);
                rightFoot.StartAnim(0);
            }
        }
    }

    private void StepAnimation(){
        if (startTime != -1f){
            Vector3 endPosition = GetOtherLeg().position;
            endPosition.x += 10f;
            endPosition.z = GetCurrentLeg().position.z;
            float t = (Time.time - startTime)/duration;
            GetCurrentLeg().position = Vector3.Lerp(startPosition, endPosition, t);

            float xPer = (t-0.5f)*2f;
            float y = startPosition.y + ( -(xPer*xPer)+2f );
            GetCurrentLeg().position = new Vector3(GetCurrentLeg().position.x, y, GetCurrentLeg().position.z);

            if (t > 1) EndStep();
        }
    }

    public Transform GetCurrentLeg(){
        if (currentLeg == 0) return leftFoot.transform;
        else return rightFoot.transform;
    }

    public Transform GetOtherLeg(){
        if (currentLeg == 1) return leftFoot.transform;
            else return rightFoot.transform;
    }

    private void ChangeLeg(){
        if (currentLeg == 0) {
            currentLeg = 1;
        } else {
            currentLeg = 0;
        }
    }

    private bool CheckOpora(){
        Transform[] rayPoints = GetCurrentLeg().gameObject.GetComponentsInChildren<Transform>();

        bool[] hitItog = new bool[2];
        bool isPillar = false;
        for (int i = 1; i < 3; i++){
            Vector3 rayOrigin = rayPoints[i].position;
            rayOrigin.y += 5f; 
            Ray ray = new Ray(rayOrigin, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit)){
                if (hit.transform.tag == "Opora") {
                    hitItog[i-1] = true;
                    pillar = hit.transform.gameObject.GetComponent<Pillar>();
                    isPillar = true; 
                }
                else if (hit.transform.tag == "Point") {
                    CheckPointGenerator point = hit.transform.gameObject.GetComponent<CheckPointGenerator>(); 
                    if (point.GenerateNext()) stateManager.GetComponentInChildren<InGameMoneyWidget>().EarnMoney(2*comboCounter.PullValue());
                    spawnPoint = hit.transform.position.x - 55f;
                    hitItog[i-1] = true;
                } else hitItog[i-1] = false;
            } else hitItog[i-1] = false;
        }

        if (hitItog[0] && hitItog[1]){
            if (isPillar){
                PushPillarId( pillar.StepOn(comboCounter, this) );
                if (pillar.CheckEnergyBuff() && duration >= 0.2f) duration -= 0.1f;
                if (pillar.CheckCounter(pointCounter.GetId())) pointCounter.IncrementPoint();
            } else PushPillarId(-1);
            return true;
        } else {
            if (currentLeg == 0) leftFoot.StartAnim(2, hitItog[0], hitItog[1]);
            else rightFoot.StartAnim(2, hitItog[0], hitItog[1]);
            GameOver();
            return false;
        }
        
    }

    private void GameOver(){
        pointCounter.Close();
        comboCounter.Close();
        if ( pillar != null ) pillar.SendGameOverToManager();
        stateManager.ChangePanel(2);
        cam.GetComponent<CameraMoving>().enabled = false;
        GetComponent<AudioSource>().Play();
        GetComponentInParent<BodyMaterialManager>().HideBody();

        Rigidbody[] rb = new Rigidbody[3];
        rb[0] = GetComponent<Rigidbody>();
        rb[1] = leftFoot.gameObject.GetComponent<Rigidbody>();
        rb[2] = rightFoot.gameObject.GetComponent<Rigidbody>();

        rb[0].drag = 0f;
        rb[0].angularDrag = 0f;
        rb[0].mass = 1f;
        rb[1].constraints = RigidbodyConstraints.None;
        rb[1].drag = 10f;
        rb[2].constraints = RigidbodyConstraints.None;
        rb[2].drag = 10f;

        this.enabled = false;
        GetComponent<BodyMoves>().enabled = false;
    }

    public float GetSpawn(){
        return spawnPoint;
    }

    private void PushPillarId(int id){
        lastPillarId[0] = lastPillarId[1];
        lastPillarId[1] = id;
    }

    public void PillarCrushed(int pillarId){
        if (this.enabled){
            if (pillarId == lastPillarId[0] || pillarId == lastPillarId[1]) GameOver();
        }
    }

}
