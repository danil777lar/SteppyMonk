using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("Distance_0")) PlayerPrefs.SetInt("Distance_0", 0);
    }
}
