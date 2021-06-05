using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    virtual public void ReactToBeat() {
        print("Reacting to Beat!");
        /* 
         * DON'T IMPLEMENT HERE.
         * The purpose of this system is to have subclasses for each enemy archetype(Grunts, Roombas, Security Cams)
         * For each subclass you create, create an `override public void ReactToBeat` function and subclass.ReactToBeat will be called instead
         * If you see this in the console, subclass.ReactToBeat is not being called
         * Reference TestEnemy.cs for help
         */
    }
}
