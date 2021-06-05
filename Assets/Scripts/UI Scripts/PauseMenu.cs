using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool paused = false;
    public GameObject pauseMenuUI;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            print("PAUSED");

            if (paused)
                Resume();
            else
                Pause();
        }
    }
    public void Resume()
    {
        paused = false;
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
    }
    void Pause()
    {
        paused = true;
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
    }

}
