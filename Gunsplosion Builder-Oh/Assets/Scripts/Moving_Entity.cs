using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Entity : MonoBehaviour
{
    public float            moveSpeed,
                            jumpForce,
                            distFromGround;
    public string           GroundLayer;

    private Rigidbody2D     rb;
    private BoxCollider2D   playerCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    internal void Move(float direction, float speed)
    {
        Vector2 newVel = rb.velocity;

        // Set x as movement
        newVel.x = direction * speed;

        // Set new velocity
        rb.velocity = newVel;
    }

    internal void Jump()
    {
        if (CanJump())
            rb.AddForce(Vector3.up * jumpForce);
    }

    private bool CanJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distFromGround, LayerMask.NameToLayer(GroundLayer));

        Debug.DrawLine(transform.position, -Vector3.up * distFromGround, Color.red);

        return (hit.collider != null);
    }
}
