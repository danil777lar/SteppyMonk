using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    public Color mainColor;

    private Renderer renderer;
    private float animDuration = 0.4f;

    private float animStart = -1f;
    private int animId = -1; // 0 - gloving off, 1 - gloving on

    bool leftPoint = false;
    bool rightPoint = false;


    void Start()
    {
        renderer = GetComponent<Renderer>(); 
        renderer.material = new Material(Shader.Find("Shader Graphs/FootShader"));
        renderer.material.SetColor("Color_EB1218D3", mainColor);
        renderer.material.SetFloat("Vector1_3C6BD517", 0f);
    }

    void Update()
    {
        switch (animId){
            case 0:
                GloveOff();
            break;
            case 1:
                GloveOn();
            break;
            case 2:
                GameOver();
            break;
        }
    }

    private void GloveOn(){
        float t = (Time.time - animStart)/animDuration;
        renderer.material.SetFloat("Vector1_3C6BD517", Mathf.Lerp(0f, 0.05f, t));

        if (t >= 1f) animId = -1;
    }

    private void GloveOff(){
        float t = (Time.time - animStart)/animDuration;
        renderer.material.SetFloat("Vector1_3C6BD517", Mathf.Lerp(0.05f, 0f, t));

        if (t >= 1f) animId = -1;
    }

    private void GameOver(){
        float t = (Time.time - animStart)/animDuration;
        float colorstep = 0f;
        if (!leftPoint && !rightPoint) colorstep = -0.01f;
        float direction = 1f;
        if (leftPoint) direction = -1f;

        renderer.material.SetFloat("Vector1_190BFB08", direction); //direction
        renderer.material.SetFloat("Vector1_3C6BD517", Mathf.Lerp(0.05f, 1f, t)); //glovelevel
        renderer.material.SetFloat("Vector1_7153798D", Mathf.Lerp(0.01f, colorstep, t)); //colorstep

        if (t >= 1f) animId = -1;
    } 

    public void StartAnim(int animId){
        this.animId = animId;
        animStart = Time.time;
    }

    public void StartAnim(int animId, bool leftPoint, bool rightPoint){
        this.animId = animId;
        this.leftPoint = leftPoint;
        this.rightPoint = rightPoint;
        animStart = Time.time;
    }
}
