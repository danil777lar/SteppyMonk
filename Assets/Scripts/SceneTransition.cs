using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private static SceneTransition instance;

    private Animator anim;
    private AsyncOperation operation;

    void Start()
    {
        instance = this;

        anim = GetComponent<Animator>();
        anim.Play("Base Layer.End");
    }

    public void SwitchScene(string sceneName){
        anim.Play("Base Layer.Start");
        operation = SceneManager.LoadSceneAsync("Scenes/"+sceneName);
        operation.allowSceneActivation = false;
    }

    public void AnimOver(){
        operation.allowSceneActivation = true;
    }
}
