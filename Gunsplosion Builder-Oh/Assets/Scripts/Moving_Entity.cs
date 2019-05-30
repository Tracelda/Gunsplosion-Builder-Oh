using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Entity : MonoBehaviour
{
    public float            moveSpeed,
                            jumpForce,
                            distFromGround,
                            gunLength;
    public GameObject       weapon;
    public GameObject       firingPoint;
    public float            maxHealth;

    internal Rigidbody2D    rb;
    internal Health         entityHealth;
    internal Weapon         weaponScript;

    private float           currentMoveSpeed;
    private BoxCollider2D   playerCollider;

    public void Start()
    {
        currentMoveSpeed = moveSpeed;

        rb = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<BoxCollider2D>();

        if (weapon)
            weaponScript = weapon.GetComponent<Weapon>();

        entityHealth = GetComponent<Health>();
        entityHealth.health = maxHealth;
    }

    /////////////////////////////////////////////////////////
    // Moves along x axis
    ////////////////////////////////////////////////////////
    internal void Move(float direction)
    {
        Vector2 newVel = rb.velocity;

        // Set x as movement
        newVel.x = direction * currentMoveSpeed;

        // Set new velocity
        rb.velocity = newVel;
    }

    /////////////////////////////////////////////////////////
    // Jump
    ////////////////////////////////////////////////////////
    internal void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    /////////////////////////////////////////////////////////
    // Checks if centity is grounded
    ////////////////////////////////////////////////////////
    internal bool CanJump()
    {
        playerCollider.enabled = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distFromGround);

        playerCollider.enabled = true;

        return (hit.collider != null);
    }

    /////////////////////////////////////////////////////////
    // Aim in direction and fire
    ////////////////////////////////////////////////////////
    internal void Aim(float x, float y)
    {
        //Debug.Log("X = " + x + "Y = " + y);
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
            //Debug.Log("newPos = " + newPos);
            newPos *= gunLength;

            firingPoint.transform.localPosition = newPos;

            weaponScript.fire();
        }
    }

    internal void Aim(float x, float y, bool shoot) {
        //Debug.Log("X = " + x + "Y = " + y);
        if (x < 0.0f)
            x = -1.0f;
        else if (x > 0.0f)
            x = 1.0f;

        if (y < 0.0f)
            y = -1.0f;
        else if (y > 0.0f)
            y = 1.0f;

        if (x != 0.0f || y != 0.0f) {
            Vector2 newPos = new Vector2(x, y);
            //Debug.Log("newPos = " + newPos);
            newPos *= gunLength;

            firingPoint.transform.localPosition = newPos;

            if (shoot)
            {
                weaponScript.fire();
            }
        }
    }
}
