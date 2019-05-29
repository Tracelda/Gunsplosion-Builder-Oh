using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public bool targetingPlayer;
    public float distance, angle;
    public GameObject playerObject;
    public Vector3 shootDirection, normShootDirection;

    private Moving_Entity Moving_Entity;

    // Start is called before the first frame update
    void Start()
    {
        Moving_Entity = gameObject.GetComponent<Moving_Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetingPlayer)
        {
            FindAngle(gameObject.transform.position, playerObject.transform.position);
            ShootAtPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            targetingPlayer = true;
            playerObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            targetingPlayer = false;
            playerObject = null;
        }
    }

    public void FindAngle(Vector3 enemyLocation, Vector3 playerPos)
    {
        shootDirection = (playerPos - enemyLocation);
        normShootDirection = Vector3.Normalize(shootDirection);
        
        RaycastHit2D hit = Physics2D.Raycast(enemyLocation, normShootDirection);
        Debug.DrawRay(enemyLocation, normShootDirection, Color.green);
    }

    public void ShootAtPlayer()
    {
        //Moving_Entity.Aim(normShootDirection.x, normShootDirection.y);
        Moving_Entity.Aim(normShootDirection.x, normShootDirection.y);
        Debug.Log("Shooting at player");
    }

    
}
