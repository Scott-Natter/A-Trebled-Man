using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class OpenDoors : MonoBehaviour
{

    public Animator anim1;
    public Animator anim2;

    void Start()
    {
        anim1 = GetComponent<Animator>();
        anim2 = GetComponent<Animator>();

    }
    public void OpenElevator()
    {
        anim1.Play("Door_Move1");
        anim2.Play("Door_Move2");
        Debug.Log("GO GO GO");
    }
}
