using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            anime.Play("walk");
        }

        if (Input.GetKeyDown("k"))
        {
            anime.Play("Idle");
        }

    }
}
