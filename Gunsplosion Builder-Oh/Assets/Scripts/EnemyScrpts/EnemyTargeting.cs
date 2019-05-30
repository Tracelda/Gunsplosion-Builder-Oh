using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public bool targetingPlayer;
    public float attackRange, distanceToPlayer, angle, normAngle;
    public GameObject playerObject;
    public Vector3 shootDirection, normShootDirection;

    public Weapon weaponScript;
    private Moving_Entity Moving_Entity;
    public BaseEnemy BaseEnemy;

    public float timer, timerTarget;
    public bool timerRunning, shoot;

    void Start()
    {
        Moving_Entity = gameObject.GetComponent<Moving_Entity>();
        weaponScript = GetComponentInChildren<Weapon>();
        BaseEnemy = GetComponentInParent<BaseEnemy>();
    }

    void Update()
    {
        if (BaseEnemy.active)
        {
            CheckForPlayer();

            if (targetingPlayer)
            {
                FindDistance();
                FindAngle(gameObject.transform.position, playerObject.transform.position);
                ShootAtPlayer(normShootDirection.x, normShootDirection.y);
                timerRunning = true;
            }
        }
    }

    public void CheckForPlayer()
    {
        var objectInRange = Physics2D.OverlapCircle(transform.position, attackRange);

        Debug.Log("Objects in range: " + objectInRange);

        if (objectInRange.gameObject.CompareTag("Player"))
        {
            targetingPlayer = true;
            playerObject = objectInRange.gameObject;

            Debug.Log("Targeted Object: " + playerObject);
        }
        if (distanceToPlayer > attackRange)
        {
            distanceToPlayer = 0;
            playerObject = null;
            targetingPlayer = false;
        }
    }

    public void FindAngle(Vector3 enemyLocation, Vector3 playerPos)
    {
        // Find angle 
        Vector2 dir = playerPos - enemyLocation;
        angle = Vector2.Angle(transform.up, dir);

        shootDirection = (playerPos - enemyLocation);
        normShootDirection = Vector3.Normalize(shootDirection);
        
        RaycastHit2D hit = Physics2D.Raycast(enemyLocation, normShootDirection);
        Debug.DrawRay(enemyLocation, normShootDirection, Color.green);
    }

    public void FindDistance()
    {
        distanceToPlayer = Vector2.Distance(playerObject.transform.position, transform.position);
    }

    private void RunTimer()
    {
        if (timerRunning)
        {
            if (timer < timerTarget)
            {
                Debug.Log("timer");
                timer += Time.deltaTime;
            }
            else if (timer >= timerTarget)
            {
                shoot = true;
                timer = 0f;

            }
        }
    }

    public void ShootAtPlayer(float x, float y)
    {
        Debug.Log("Shooting at player");

        // target up
        if (angle < 22.5f)
        {
            x = 0f;
            y = 1f;
        }
        // target down
        else if (angle > 157.5f)
        {
            x = 0f;
            y = -1f;
        }
        // target right
        else if (shootDirection.x > 0)
        {
            // target 90 degrees
            if (angle > 67.5f && angle < 112.5f)
            {
                x = 1f;
                y = 0f;
            }
            // target 45 degrees
            else if (angle < 67.5f && angle > 22.5f)
            {
                x = 1f;
                y = 1f;
            }
            // target 135 degrees
            else if (angle > 112.5f && angle < 157.5f)
            {
                x = 1f;
                y = -1f;
            }
        }
        // target left
        else if (shootDirection.x < 0)
        {
            // target 90 degrees
            if (angle > 67.5f && angle < 112.5f)
            {
                x = -1f;
                y = 0f;
            }
            // target 45 degrees
            else if (angle < 67.5f && angle > 22.5f)
            {
                x = -1f;
                y = 1f;
            }
            // target 135 degrees
            else if (angle > 112.5f && angle < 157.5f)
            {
                x = -1f;
                y = -1f;
            }
        }

        Vector2 newPos = new Vector2(x, y);
        newPos *= Moving_Entity.gunLength;

        Moving_Entity.firingPoint.transform.localPosition = newPos;

        RunTimer();

        if (shoot)
        {
            weaponScript.fire();
            shoot = false;
        }
    }
}
