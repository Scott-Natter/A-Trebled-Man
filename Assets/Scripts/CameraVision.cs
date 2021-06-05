using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject VisionCollider;
    // Update is called once per frame
    //int step = 0;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    void Start()
    {
        //gameObject.GetComponentInChildren<Light>().spotAngle = ((Mathf.Atan(gameObject.transform.localScale.x) * 180)/Mathf.PI)/10;
    }

    private void OnTriggerEnter(Collider other)
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Grunt");
        if(other.gameObject.tag == "Player")
        {
            /*
            foreach (GameObject badguy in enemies)
            {
                badguy.GetComponent<EnemyMove>().CameraUpdate(other);
            }
            */
            print("Spotted");
            if (enemy1 != null)
                enemy1.GetComponent<EnemyMove>().SetAlertState();
            if (enemy2 != null)
                enemy2.GetComponent<EnemyMove>().SetAlertState();
            if (enemy3 != null)
                enemy3.GetComponent<EnemyMove>().SetAlertState();
        }
        else if (other.gameObject.tag == "GridTile") {
            other.gameObject.GetComponent<GridTile>().inVision = false;
            other.gameObject.GetComponent<GridTile>().inEnemyVision = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GridTile")
        {
            other.gameObject.GetComponent<GridTile>().inVision = false;
            other.gameObject.GetComponent<GridTile>().inEnemyVision = false;
        }
    }
}
