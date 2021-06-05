using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tension : MonoBehaviour
{
    public AudioSource tensionSound;

    // Start is called before the first frame update
    void Start()
    {
        tensionSound.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onTriggerEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Grunt")
        {
            Debug.Log("Im tense right now");
            tensionSound.mute = false;
        }
    }
}
