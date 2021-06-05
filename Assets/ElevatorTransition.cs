using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ElevatorTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public string targetScene;
    void Start()
    {
        StartCoroutine(WaitBeforeTransition());
    }
    IEnumerator WaitBeforeTransition()
    {
        yield return new WaitForSeconds(5f);
        print("Transitioning");
        GameObject target = GameObject.FindGameObjectWithTag("SceneTarget");
        if (target != null && target.GetComponent<TargetScene>().NextSceneName != null)
        {
            targetScene = target.GetComponent<TargetScene>().NextSceneName;
            print(targetScene);
        }
        Destroy(GameObject.FindGameObjectWithTag("SceneTarget"));
        
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        if (targetScene != null)
            SceneManager.LoadScene(targetScene);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
