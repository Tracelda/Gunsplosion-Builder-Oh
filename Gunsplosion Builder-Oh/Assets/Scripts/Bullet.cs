using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon.bulletType currentType;
    public bool active;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float lifeSpan;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            switch (currentType.weaponType)
            {
                case Weapon.weaponType.Straight:
                    straightShot();
                    break; 
                case Weapon.weaponType.Bounce:
                    bounceShot();
                    break;
                case Weapon.weaponType.Homing:
                    homingShot();
                    break;
                default:
                    break;
            }
            lifeSpan -= Time.deltaTime;
            if(lifeSpan <0)
            {
                deActivate();
            }
            
        }
    }

    public void activate(Weapon.bulletType type, Vector2 position)
    {
        active = true;
        currentType = type;
        spriteRenderer.sprite = currentType.bulletSprite;
        lifeSpan = currentType.lifeSpan;
    }
    public void deActivate()
    {
        active = false;
        spriteRenderer.sprite = null;
        rb.velocity = Vector2.zero;
    }

    private void straightShot()
    {
        rb.velocity = Vector2.up * currentType.bulletSpeed;
    }
    private void homingShot()
    {

    }
    private void bounceShot()
    {

    }



    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (other.gameObject)
            {
                if (other.gameObject.GetComponent<Health>())
                {
                    other.gameObject.GetComponent<Health>().takeDamage(currentType.damage);
                    deActivate();
                }
            }
        }
    }
}
