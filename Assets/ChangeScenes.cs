using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("Logo");
        }
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("TitleScreen");
        }
        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("Intro-Movement");
        }
        if (Input.GetKeyDown("4"))
        {
            SceneManager.LoadScene("Grunt_Intro(MOVEMENT)");
        }
        if (Input.GetKeyDown("5"))
        {
            SceneManager.LoadScene("Grunt_Intro(TAKEDOWN)");
        }
        if (Input.GetKeyDown("6"))
        {
            SceneManager.LoadScene("Cover_Intro");
        }
        if (Input.GetKeyDown("7"))
        {
            SceneManager.LoadScene("Intro_Cameras_Level");
        }
        if (Input.GetKeyDown("8"))
        {
            SceneManager.LoadScene("IntroLevel - (EXPANDED)");
        }
        if (Input.GetKeyDown("9"))
        {
            SceneManager.LoadScene("Office");
        }
        if (Input.GetKeyDown("0"))
        {
            SceneManager.LoadScene("Elevator");
        }
    }
}
