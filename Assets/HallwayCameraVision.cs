using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayCameraVision : MonoBehaviour
{
    private int step = 1;
    public bool isOn = true;
    public GameObject collider1;
    public GameObject collider2;
    private int beat;
    //0,3,6,9
    public int[] beatsToSwivel;
    public int totalBeatCount;
    void Start()
    {
        //start = gameObject.transform.position.z;
        //print("Hello Start");
        beat = 0;
        TimeTicker.OnTick += delegate (object sender, TimeTicker.OnTickEventArgs e)
        {
            DetermineCameraSwivel();
        };

    }
    private void DetermineCameraSwivel()
    {
         for(int i = 0; i < beatsToSwivel.Length; i++)
        {
            print("Hello Determine");
            if(beatsToSwivel[i] == beat)
            {
                RotateCamera();
            }
        }
            beat++;
            if(beat >= totalBeatCount)
            beat = 0;
    }
    private void Update()
    {
        //gameObject.GetComponentInChildren<Light>().spotAngle = ((Mathf.Atan(gameObject.transform.localScale.x / 5) / Mathf.PI) * 180);
    }
    private void RotateCamera()
    {
        if (isOn)
        {
            switch (step)
            {
                case 1:
                    //gameObject.transform.GetChild(0).gameObject.transform.localPosition.z = start;
                    //gameObject.transform.position.z = start;
                    collider1.SetActive(true);
                    collider2.SetActive(false);
                    step++;
                    break;
                case 2:
                    collider1.SetActive(false);
                    collider2.SetActive(false);
                    step++;
                    break;
                case 3:
                    collider1.SetActive(false);
                    collider2.SetActive(true);
                    step++;
                    break;
                case 4:
                    collider1.SetActive(false);
                    collider2.SetActive(false);
                    step = 1;
                    break;
            }
            /*
            if (step == numberRotations)
            {
                gameObject.transform.eulerAngles = start;
                step = 0;
            }
            else
            {
                gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotation, transform.eulerAngles.z);
                step++;
            }
            */
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Grunt");
        if (other.gameObject.tag == "Player")
        {
            print("hit");
        }

    }

}
