using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Shop shop;

    public Color color;
    public float burningLevel = -4f;
    public bool choosed = false;
    public bool clicable = false;

    [SerializeField]
    private Shader shader;

    private Renderer bodyRenderer;
    private Renderer topRenderer;
    private Animator animator;

    private GameObject reward;
    private int maskId = -1;

    private void Start(){
        bodyRenderer = GetComponent<Renderer>();
        topRenderer = GetComponentsInChildren<Renderer>()[1];
        animator = GetComponent<Animator>();

        bodyRenderer.material = new Material(shader);
        bodyRenderer.material.SetColor("Color_EB1218D3", color);
        bodyRenderer.material.SetFloat("Vector1_C40376B7", 1f);
        bodyRenderer.material.SetFloat("Vector1_ED51A5EF", burningLevel);

        topRenderer.material = new Material(Shader.Find("Shader Graphs/ChestShader"));
        topRenderer.material.SetColor("Color_EB1218D3", color);
        topRenderer.material.SetFloat("Vector1_C40376B7", 1f);
        topRenderer.material.SetFloat("Vector1_ED51A5EF", burningLevel);
    }

    private void Update(){
        bodyRenderer.material.SetFloat("Vector1_ED51A5EF", burningLevel);
        topRenderer.material.SetFloat("Vector1_ED51A5EF", burningLevel);
    }

    public void ConnectShop(Shop sh){
        shop = sh;
    }

    public void ShowChest(int id){
        maskId = id;
        Destroy(reward);
        if (!choosed) animator.Play("Base Layer.ShowChest");
        clicable = true;

        reward = Instantiate(Resources.LoadAll<Mask>("Objects/Masks/")[id].gameObject);
        reward.transform.parent = GetComponentsInChildren<Transform>()[1];
        reward.transform.localPosition = new Vector3(0f, 0f, 0f);
        reward.transform.localScale = new Vector3(10f, 10f, 10f);
        reward.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        reward.SetActive(false);
        Invoke("InvokeShowChest", 1f);
    }

    public void ShowChest(){
        clicable = true;
        if (choosed) Restart();
    }

    private void InvokeShowChest(){
        reward.SetActive(true);
    }

    public void ChooseChest(){
        animator.Play("Base Layer.OpenChest");
        choosed = true;
        shop.SetReward(maskId);
    }

    public void Restart(){
        if (choosed) animator.Play("Base Layer.CloseChest");
        choosed = false;
        clicable = false;
        maskId = -1;
        Invoke("InvokeRestart", 1f);
    }

    private void InvokeRestart(){
        Destroy(reward);
        animator.Play("HidChest");
    }
}
