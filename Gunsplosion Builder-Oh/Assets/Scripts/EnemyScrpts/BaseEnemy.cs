using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BlockResetBase
{
    public Rigidbody2D rigid;
    private Vector3 Startpos;
    public bool active;

    public void StartGame()
    {
        rigid = GetComponent<Rigidbody2D>();
        Startpos = transform.position;
        rigid.simulated = true;
        active = true;

    }

    public void ResetToEdit()
    {
        transform.position = Startpos;
        rigid.simulated = false;
        rigid.velocity = Vector2.zero;
        active = false;
    }

}
