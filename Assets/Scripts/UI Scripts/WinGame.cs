using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    public GameObject winGameUI;

    public void resetGame()
    {
        print("Cover me i'm RELOADING!");
        winGameUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("IntroLevel - (EXPANDED)");  // We can change this to be more dynamic later
    }
}
