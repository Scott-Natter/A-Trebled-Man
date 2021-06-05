using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private int step = 0;
    public int rotation = 45;
    public int numberRotations = 3;
    public bool isOn = true;
    private Vector3 start;
    private int beat;
    public int[] beatsToSwivel;
    public int totalBeatCount;
    void Start()
    {
        start = gameObject.transform.eulerAngles;
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
            //print("Hello Determine");
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
        /*
        if(!isOn)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

        */
    }

    private void RotateCamera()
    {
        if(isOn)
        {
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
        }
    }

    private void OnDestroy()
    {
        TimeTicker.OnTick -= delegate (object sender, TimeTicker.OnTickEventArgs e)
        {
            RotateCamera();
        };
    }
}
