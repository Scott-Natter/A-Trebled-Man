using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwaySurveillanceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private int step = 1;
    private int rotation = 1;
    //private int numberRotations = 4;
    public bool isOn = true;
    //private float start;
    public GameObject collider1;
    public GameObject collider2;
    void Start()
    {
        //start = gameObject.transform.position.z;
        TimeTicker.OnTick += delegate (object sender, TimeTicker.OnTickEventArgs e)
        {
            RotateCamera();
        };

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
        if (isOn)
        {
            switch(step)
            {
                case 1:
                    //gameObject.transform.GetChild(0).gameObject.transform.localPosition.z = start;
                    //gameObject.transform.position.z = start;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    step++;
                    break;
                case 2:
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    step++;
                    break;
                case 3:
                    gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - rotation);
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    step++;
                    break;
                case 4:
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    step++;
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
}
