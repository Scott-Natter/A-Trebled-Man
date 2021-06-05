using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntVision : MonoBehaviour
{

    public bool seeMaestro = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            seeMaestro = true;
        }

        if (collider.gameObject.tag == "GridTile" && collider.GetComponent<GridTile>().inVision)
        {
            collider.GetComponent<GridTile>().inEnemyVision = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            seeMaestro = false;
        }

        if (collider.gameObject.tag == "GridTile" && collider.GetComponent<GridTile>().inVision)
        {
            collider.GetComponent<GridTile>().inEnemyVision = false;
        }
    }
}
