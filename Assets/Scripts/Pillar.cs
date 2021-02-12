﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public Color mainColor;

    private Renderer renderer;
    private float animDuration = 0.3f;

    private float animStart = -1f;
    private int animStep = 0;

    private int counterId = -1;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Shader Graphs/PillarShaderPBR"));
        renderer.material.SetColor("Color_60E41B3B", mainColor);
        renderer.material.SetFloat("Vector1_3D3B3EC7", 1f);
    }

    private void Update(){
        if (animStart != -1f) ShineAnim();
    }

    private void ShineAnim(){
        float t = (Time.time - animStart)/animDuration;
        if (animStep == 0) renderer.material.SetFloat("Vector1_F6818F05", Mathf.Lerp(-0.1f, 0.1f, t));//shinepart to 0.1
        else if (animStep == 1) renderer.material.SetFloat("Vector1_25E8DD92", Mathf.Lerp(-1f, 1f, t));//direction to 1
        else if (animStep == 2) renderer.material.SetFloat("Vector1_F6818F05", Mathf.Lerp(0.1f, -0.1f, t));//shinepart to -0.1

        if (t >= 1f && animStep < 3) {
            animStart = Time.time;
            animStep++;
        } else if (t >= 1f && animStep >= 3){
            animStart = -1f;
            animStep = 0; 
            renderer.material.SetFloat("Vector1_25E8DD92", -1f);
        }
    }

    public void Shine(){
        animStart = Time.time;
    }

    public bool CheckCounter(int counterId){
        if (this.counterId != counterId){
            this.counterId = counterId;
            return true;
        }
        return false;
    }


}
