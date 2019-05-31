using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BlockResetBase
{
    public Rigidbody2D rigid;
    private Vector3 Startpos;
    public bool active;
    public bool onScreen;
    public bool MovingRight, mirrorPatroling = false;

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
        foreach (Behaviour comp in gameObject.GetComponents<Behaviour>())
        {
            comp.enabled = true;
        }
        GetComponent<Renderer>().enabled = true;
        GetComponent<Health>().health = GetComponent<Moving_Entity>().maxHealth;
    }

    public void FlipSprite()
    {
        if (MovingRight)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
