using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField]
    private Color mainColor;
    [SerializeField]
    private int lenght;

    private GameObject celledPillar;
    private AudioSource audio;
    private Renderer renderer;
    private Animator animator;
    private PillarComboManager pillarComboManager;
    private float animDuration = 0.3f;

    private float animStart = -1f;
    private int animStep = 0;

    private Vector3 fixedPosition;

    private int counterId = -1;

    private bool energy = false;
    private bool energyEnabled = false;
    private bool celled = false;

    // Animator values
    public float shine = 0f;
    public float energyBuff = 0f;
    public float yOffset = 0f;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/Keylimba/"+Random.Range(1, 7));

        animator = GetComponent<Animator>();
        pillarComboManager = GetComponentInParent<PillarComboManager>();
        renderer = GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Shader Graphs/PillarShaderPBR"));
        renderer.material.SetColor("Color_60E41B3B", mainColor);
        renderer.material.SetFloat("Vector1_6F23C3F7", Random.Range(0f, 2f));
        if (energy) animator.Play("Base Layer.EnergyOn");
    }

    private void FixPosition(){
        fixedPosition = transform.localPosition;
    }

    private void Update(){
        renderer.material.SetFloat("Vector1_3D3B3EC7", shine);
        renderer.material.SetFloat("Vector1_8541B8CA", energyBuff);
        if (yOffset != 0f) transform.localPosition = new Vector3(fixedPosition.x, fixedPosition.y-yOffset, fixedPosition.z);
    }

    public int StepOn(ComboCounter counter, WalkManager walkManager){
        if (energy && energyEnabled) {
            energyEnabled = false;
            animator.Play("Base Layer.EnergyOff");
        } else if (celled){
            ShakePillar();
            Invoke("CrushPillar", 2f);
        }
        audio.Play();
        return pillarComboManager.PillarStepped(this, counter, walkManager);
    }

    

    public void SetModification(int mod){
        if (mod == 1) {
            energy = true;
            energyEnabled = true;
        }
        else if (mod == 2) celled = true;
    }

    // Energy Buff
    public bool CheckEnergyBuff(){
        if (energy) return true;
        else return false;
    }

    // Celled functionns 
    private void ShakePillar(){
        FixPosition();
        animator.SetBool("Shake", true);
    }

    private void CrushPillar(){
        if (animator.GetBool("Shake")){
            pillarComboManager.PillarCrushed(this);
            animator.SetBool("Shake", false);
            renderer.enabled = false;
            GetComponent<MeshCollider>().enabled = false;
            celledPillar = Instantiate(Resources.Load<GameObject>("Objects/Pillars/Celled/celled_"+lenght));
            celledPillar.transform.parent = transform;
            celledPillar.transform.localPosition = new Vector3(0f, 0f, 0f);
            celledPillar.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

            Renderer[] rends = celledPillar.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < rends.Length; i++){
                rends[i].material = renderer.material;
            }
        }
    }

    // Combo functions
    public bool CheckCounter(int counterId){
        if (this.counterId != counterId){
            this.counterId = counterId;
            return true;
        }
        return false;
    }
    public void ComboAdd(){
        animator.Play("Base Layer.ShineOn");
    }
    public void ComboLose(){
        animator.Play("Base Layer.ShineOff");
    }
    public void ComboComplete(){
        animator.Play("Base Layer.ComboComplete");
    }

    public void SendGameOverToManager(){
        GetComponentInParent<PillarComboManager>().RestartPillars();
    }


    public void Restart(){
        Invoke("TrueRestart", 2f);
    }

    private void TrueRestart(){
        if (celled && (!renderer.enabled || animator.GetBool("Shake"))){
            animator.SetBool("Shake", false); 
            Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < rbs.Length; i++){
                rbs[i].isKinematic = true;
            }
            animator.Play("Restart Layer.Restart");
            Invoke("RestartCelled", .5f);
        }
        if (energy && !energyEnabled){
            animator.Play("Base Layer.EnergyOn");
            energyEnabled = true;
        }
    }

    private void RestartCelled(){
        renderer.enabled = true;
        GetComponent<MeshCollider>().enabled = true;
        if (celledPillar != null) Destroy(celledPillar);
    }
}
