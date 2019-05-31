using UnityEngine;

public class Moving_Entity : MonoBehaviour
{
    public float            moveSpeed,
                            jumpForce,
                            distFromGround,
                            gunLength;
    public GameObject       weapon;
    public GameObject       firingPoint;
    public int              maxHealth;

    internal Rigidbody2D    rb;
    internal Health         entityHealth;
    internal Weapon         weaponScript;
    internal float          currentMoveSpeed;
    internal bool           invincible;

    private BoxCollider2D   entityCollider;

    public void Start()
    {
        currentMoveSpeed = moveSpeed;

        rb = GetComponent<Rigidbody2D>();

        entityCollider = GetComponent<BoxCollider2D>();

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

    internal void MoveVector3(Vector3 Object, Vector3 Direction)
    {
        Vector3.MoveTowards(Object, Direction, moveSpeed);
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
        //entityCollider.enabled = false;

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, entityCollider.bounds.size, 0.0f, -Vector3.up, distFromGround);

        //entityCollider.enabled = true;

        return (hit.collider != null);
    }

    /////////////////////////////////////////////////////////
    // Aim in direction and fire
    ////////////////////////////////////////////////////////
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

            firingPoint.transform.localPosition = newPos;

            weaponScript.fire();
        }
    }

    internal void Aim(float x, float y, bool shoot) {
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
            newPos *= gunLength;

            firingPoint.transform.localPosition = newPos;

            if (shoot)
            {
                weaponScript.fire();
            }
        }
    }
}
