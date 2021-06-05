using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRecord : MonoBehaviour
{
    public GameObject triggerWinUI;
    public GameObject record;
    public GameObject Maestro;
    public float waitTime;
    public float animTime;

    private IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(waitTime);
        record.SetActive(true);
        StartCoroutine(WaitToDespawn());
    }

    private IEnumerator WaitToDespawn()
    {
        yield return new WaitForSeconds(animTime);
        record.SetActive(false);
        triggerWinUI.SetActive(true);
        Maestro.GetComponent<PlayerMove>().enabled = false;
    }

    public void Record()
    {
        StartCoroutine(WaitToSpawn());
    }
}
