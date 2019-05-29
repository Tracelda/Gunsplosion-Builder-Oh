using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoundScript : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Moving_Entity>().entityHealth.takeDamage(damage);
        }
    }
}
