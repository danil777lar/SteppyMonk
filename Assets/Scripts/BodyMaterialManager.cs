using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMaterialManager : MonoBehaviour
{
    public float intensity = 1.5f;
    [SerializeField] private Material bodyMaterial;
    [SerializeField] private Renderer leftfoot;
    [SerializeField] private Renderer rightfoot;

    private Animation anim;
    private WalkManager walkManager;

    void Start()
    {
        anim = GetComponent<Animation>();
        walkManager = GetComponentInChildren<WalkManager>();
        walkManager.SetWalkable(false);
        ShowBody();
    }

    void Update()
    {
        bodyMaterial.SetFloat("Vector1_C0B1C65F", intensity);
        leftfoot.material.SetFloat("Vector1_498AC3A7", intensity);
        rightfoot.material.SetFloat("Vector1_498AC3A7", intensity);
    }

    public void ShowBody(){
        anim.Play("ShowBody");
        Invoke("Go", 3f);
    }

    private void Go(){
        walkManager.SetWalkable(true);
    }

    public void HideBody(){
        anim.Play("HideBody");
    }
}
