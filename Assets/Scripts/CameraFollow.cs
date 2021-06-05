using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Source: https://github.com/Brackeys/Smooth-Camera-Follow/blob/master/Smooth%20Camera/Assets/CameraFollow.cs
public class CameraFollow : MonoBehaviour
{
    public Transform playerPositon;
    public Vector3 offset;    
    public float smoothSpeed = 0.125f;
    public Text zSlide;
    public Text ySlide;

    // Update is called once per frame
    void Update()
    {
        if (playerPositon != null)
        {
            Vector3 desiredPosition = playerPositon.position - offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(playerPositon);
        }
    }
    public void AdjustY(float newY)
    {
        offset.y = newY;
        ySlide.text = newY.ToString();
    }
    public void AdjustZ(float newZ)
    {
        offset.z = newZ;
        zSlide.text = newZ.ToString();
    }
}
