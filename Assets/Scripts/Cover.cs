using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Any object that can give Maestro cover
public class Cover : MonoBehaviour
{
    public float proxThreshold = 2f;

    private PlayerMove player;

    void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    private void Update()
    {
        ToggleAllowCover();
    }

    bool MaestroIsClose()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        print(dist);
        if (Vector3.Distance(player.transform.position, transform.position) < proxThreshold)
        {
            return true;
        }
        return false;
    }

    private void ToggleAllowCover()
    {
        if (MaestroIsClose())
        {
            // Present UI to show that Maestro can input for cover
            player.nearbyCover = this;
            print("Maetro is close! Allow cover input");
        }

        else
        {
            player.nearbyCover = null;
            //print("Maetro is far! No input!");
        }
    }
}
