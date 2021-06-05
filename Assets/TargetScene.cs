using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string NextSceneName;
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
