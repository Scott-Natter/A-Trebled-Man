using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles interaction between player inputs and cover
// Allow Cover to turn itself off
public class CoverVisionManager : MonoBehaviour
{
    private Collider normalVision;
    private Collider leftVision;
    private Collider rightVision;

    void Start()
    {
        normalVision = transform.GetChild(1).GetComponent<Collider>();
        leftVision = transform.GetChild(2).GetComponent<Collider>();
        rightVision = transform.GetChild(3).GetComponent<Collider>();
    }

    public void DisableNormalVision()
    {
        normalVision.gameObject.SetActive(false);
    }

    public void EnableNormalVision()
    {
        DisableLeftVision();
        DisableRightVision();

        normalVision.gameObject.SetActive(true);
    }

    public void EnableLeftVision()
    {
        DisableRightVision();
        leftVision.gameObject.SetActive(true);
    }

    public void DisableLeftVision()
    {
        leftVision.gameObject.SetActive(false);
    }

    public void EnableRightVision()
    {
        DisableLeftVision();
        rightVision.gameObject.SetActive(true);
    }

    public void DisableRightVision()
    {
        rightVision.gameObject.SetActive(false);
    }
}