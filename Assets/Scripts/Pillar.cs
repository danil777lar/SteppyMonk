using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField]
    private Color mainColor;

    private AudioSource audio;
    private Renderer renderer;
    private Animator animator;
    private float animDuration = 0.3f;

    private float animStart = -1f;
    private int animStep = 0;

    private int counterId = -1;

    private bool energy = false;

    public float shine = 0f;
    public float energyBuff = 0f;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/Keylimba/"+Random.Range(1, 7));

        animator = GetComponent<Animator>();

        renderer = GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Shader Graphs/PillarShaderPBR"));
        renderer.material.SetColor("Color_60E41B3B", mainColor);
        if (energy) animator.Play("Base Layer.EnergyOn");
    }

    private void Update(){
        renderer.material.SetFloat("Vector1_3D3B3EC7", shine);
        renderer.material.SetFloat("Vector1_8541B8CA", energyBuff);
    }

    public void StepOn(ComboCounter counter){
        if (energy) {
            energy = false;
            animator.Play("Base Layer.EnergyOff");
            Debug.Log("ENERGYOFF");
        }
        audio.Play();
        GetComponentInParent<PillarComboManager>().PillarStepped(this, counter);
    }

    public bool CheckCounter(int counterId){
        if (this.counterId != counterId){
            this.counterId = counterId;
            return true;
        }
        return false;
    }

    public bool CheckEnergyBuff(){
        if (energy) return true;
        else return false;
    }
    public void SetEnergy(){
        energy = true;
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

    public void GameOver(){
        GetComponentInParent<PillarComboManager>().RestartPillars();
    }
}
