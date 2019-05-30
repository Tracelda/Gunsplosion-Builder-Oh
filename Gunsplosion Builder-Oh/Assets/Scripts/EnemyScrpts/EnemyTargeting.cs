using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public bool targetingPlayer;
    public float attackRange, distanceToPlayer;
    public GameObject playerObject;
    public Vector3 shootDirection, normShootDirection;

    public Weapon weaponScript;
    private Moving_Entity Moving_Entity;

    // Start is called before the first frame update
    void Start()
    {
        Moving_Entity = gameObject.GetComponent<Moving_Entity>();
        weaponScript = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();

        if (targetingPlayer)
        {
            FindDistance();
            FindAngle(gameObject.transform.position, playerObject.transform.position);
            ShootAtPlayer(normShootDirection.x, normShootDirection.y);
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
        shootDirection = (playerPos - enemyLocation);
        normShootDirection = Vector3.Normalize(shootDirection);
        
        RaycastHit2D hit = Physics2D.Raycast(enemyLocation, normShootDirection);
        Debug.DrawRay(enemyLocation, normShootDirection, Color.green);
    }

    public void FindDistance()
    {
        distanceToPlayer = Vector2.Distance(playerObject.transform.position, transform.position);
    }

    public void ShootAtPlayer(float x, float y)
    {
        //Moving_Entity.Aim(normShootDirection.x, normShootDirection.y);
        Debug.Log("Shooting at player");

        //if (((x > -0.75f && x < 0.25f) && (y > 0.0f && y < 0.25f)) || ((x < 0.0f && x > -0.25f) && (y < 0.0f && y > -0.25f))) // target right 0 degrees
        //{
        //    Debug.Log("target right 0 degrees");
        //    x = 1f;
        //    y = 0f;
        //}
        //else if (((x > 0.25f && x < 0.75f) && (y > 0.25f && y < 0.75f))) // target right 45 degrees
        //{
        //    Debug.Log("target right 45 degrees");
        //    x = 1f;
        //    y = 1f;
        //}
        //else if (((x > 0.0f && x < 0.25f) && (y > 0.75f && y < 0.1f)) || ((x < 0.0f && x > -0.25f) && (y < 0.75f && y < 1f))) // target up 90 degrees
        //{
        //    Debug.Log("target up 90 degrees");
        //    x = 0f;
        //    y = 1f;
        //}
        //else if (((x > 0.25f && x < 0.75f) && (y > -0.25f && y < -0.75f))) // target right -45 degrees
        //{
        //    Debug.Log("target right -45 degrees");
        //    x = -1f;
        //    y = -1f;
        //}

        if (x < 0.5f && x > -0.5f)
            x = 0f;
        else if (x > 0.5f)
            x = 1.0f;
        else if (x < -0.5f)
            x = -1.0f;

        if (y > -0.25f && y < 0.25f) // target left & right 0 degrees
        {
            y = 0f;
        }
        else if (y > 0.25f && y < 0.75f)
        {
            y = 1f;
        }
        else if (y > 0.75f && (x < 0.5f && x > -0.5f))
        {
            y = 1f;
            x = 0f;
        }

        if (x != 0.0f && y != 0.0f)
        {
            Vector2 newPos = new Vector2(x, y);
            //Debug.Log("newPos = " + newPos);
            newPos *= Moving_Entity.gunLength;

            Moving_Entity.firingPoint.transform.localPosition = newPos;

            weaponScript.fire();
        }
    }
    
    
}
