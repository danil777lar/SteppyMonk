using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    private RectTransform trans;

    protected void Start(){
        trans = GetComponent<RectTransform>();
    }

    public virtual void Close(){
        Invoke("TrueClose", 1f);
        Animator[] anim = GetComponentsInChildren<Animator>();
        for (int i = 0; i < anim.Length; i++){
            anim[i].Play("Base Layer.Close");
        }
    }

    private void TrueClose(){
        trans.anchorMin = new Vector2(-1f, -1f);
        trans.anchorMax = new Vector2(0f, 0f);
        trans.offsetMax = new Vector2(0f, 0f);
        trans.offsetMin = new Vector2(0f, 0f);
    }

    public virtual void Open(){
        trans.anchorMin = new Vector2(0f, 0f);
        trans.anchorMax = new Vector2(1f, 1f);
        trans.offsetMax = new Vector2(0f, 0f);
        trans.offsetMin = new Vector2(0f, 0f);
        Animator[] anim = GetComponentsInChildren<Animator>();
        for (int i = 0; i < anim.Length; i++){
            anim[i].Play("Base Layer.Open");
        }
    }
}
