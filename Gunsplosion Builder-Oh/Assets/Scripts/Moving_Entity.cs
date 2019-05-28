using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Entity : MonoBehaviour
{
    public float            moveSpeed,
                            jumpForce,
                            distFromGround,
                            gunLength;
    public GameObject       firingPoint;

    internal Rigidbody2D    rb;

    private float           currentMoveSpeed;
    private BoxCollider2D   playerCollider;

    public void Start()
    {
        currentMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    internal void Move(float direction)
    {
        Vector2 newVel = rb.velocity;

        // Set x as movement
        newVel.x = direction * currentMoveSpeed;

        // Set new velocity
        rb.velocity = newVel;
    }

    internal void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    internal bool CanJump()
    {
        playerCollider.enabled = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distFromGround);

        playerCollider.enabled = true;

        Debug.DrawLine(transform.position, hit.point, Color.red);

        return (hit.collider != null);
    }

    internal void Aim(float x, float y)
    {
        if (x < 0.0f)
            x = -1.0f;
        else if (x > 0.0f)
            x = 1.0f;

        if (y < 0.0f)
            y = -1.0f;
        else if (y > 0.0f)
            y = 1.0f;

        if (x != 0.0f || y != 0.0f)
        {
            Vector2 newPos = new Vector2(x, y);
            newPos *= gunLength;

            Debug.Log(newPos);

            firingPoint.transform.position = newPos;
            //firingPoint.transform.position *= gunLength;
        }
    }
}
