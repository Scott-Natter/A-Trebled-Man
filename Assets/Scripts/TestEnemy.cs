using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    Vector3 test = new Vector3(1, 1, 1);
    EnemyMove move;

    private void Start()
    {
        move = gameObject.GetComponent<EnemyMove>();
    }

    override public void ReactToBeat()
    {
        //print("Move one tile");
        move.Move();
    }
}
