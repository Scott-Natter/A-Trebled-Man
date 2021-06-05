using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public GameObject resetGameUI;

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
