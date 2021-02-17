﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField]
    private Material bodyMaterial;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Distance_0")) PlayerPrefs.SetInt("Distance_0", 0);
        if (!PlayerPrefs.HasKey("Max_Points")) PlayerPrefs.SetInt("Max_Points", 0);
        if (PlayerPrefs.HasKey("Money")) PlayerPrefs.SetInt("Money", 1047);

        if (!PlayerPrefs.HasKey("Mask")) PlayerPrefs.SetInt("Mask", 0);
        if (!PlayerPrefs.HasKey("Inventory")) PlayerPrefs.SetString("Inventory", "0");
    }

    private void OnDestroy(){
        bodyMaterial.SetFloat("Vector1_C0B1C65F", 1.3f);
    }
}
