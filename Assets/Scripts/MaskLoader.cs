using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskLoader : MonoBehaviour
{
    private GameObject mask;

    void Start()
    {
        LoadMask(PlayerPrefs.GetInt("Mask"));
    }

    public void LoadMask(int id){

        if (mask != null) Destroy(mask);

        mask = Instantiate(Resources.LoadAll<Mask>("Objects/Masks/")[id].gameObject);
        mask.transform.parent = transform;
        mask.transform.localPosition = new Vector3(0f, 0f, 0f);
        mask.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        mask.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public Mask GetMask(){
        return mask.GetComponent<Mask>();
    }
}
