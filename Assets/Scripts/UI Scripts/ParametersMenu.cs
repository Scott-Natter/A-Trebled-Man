using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersMenu : MonoBehaviour
{
    public GameObject parameterMenuUI;
    public GameObject pauseMenu;

    public void DisplayUI()
    {
        pauseMenu.SetActive(false);
        parameterMenuUI.SetActive(true);
    }

    public void DismissUI() {
        pauseMenu.SetActive(true);
        parameterMenuUI.SetActive(false);
    }
}
