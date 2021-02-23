using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{
    private Text text;

    private int value = 1;

    private void Start(){
        text = GetComponent<Text>();
        text.text = "" + value;
    }

    public void UpdateValue(int newValue){
        value = newValue;
        text.text = "" + value;
    }
}
