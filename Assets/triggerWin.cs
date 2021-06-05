using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerWin : MonoBehaviour
{
    public GameObject winGameUI;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("heredumb {0}", other.transform.rotation);
        if(other.tag == "Player" && other.transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            other.GetComponent<PlayerMove>().RecordAnimation();
        }
    }

    public void HomeScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
