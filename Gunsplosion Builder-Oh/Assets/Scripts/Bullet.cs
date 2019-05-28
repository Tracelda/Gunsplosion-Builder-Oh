using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon.BulletType currentType;
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

    public void activate(Weapon.BulletType type, Vector2 position, Vector2 aimDirection)
    {
        active = true;
        currentType = type;
        spriteRenderer.sprite = currentType.bulletSprite;
        lifeSpan = currentType.lifeSpan;
        gameObject.transform.localPosition = position; //= position;
        transform.rotation = lookAt2D(aimDirection);
        //gameObject.transform.localRotation = ;
    }
    public void deActivate()
    {
        active = false;
        spriteRenderer.sprite = null;
        rb.velocity = Vector2.zero;
    }

    private void straightShot()
    {
        rb.velocity = transform.up * currentType.bulletSpeed;
    }
    private void homingShot()
    {
        rb.velocity = transform.up * currentType.bulletSpeed;
        var hit = Physics2D.CircleCast(transform.position, 2, Vector2.up);
        print(hit.collider);
        if(hit)
        {
            if(hit.collider.gameObject)
            {
                if (hit.collider.gameObject.GetComponent<Health>())
                {
                    Quaternion toRotation = lookAt2D(hit.point);
                    //transform.rotation = toRotation;
                    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1.0f * Time.deltaTime);
                }
            }
        }
    }
    private void bounceShot()
    {

    }

    private Quaternion lookAt2D(Vector2 WorldPos)
    {
        Vector3 tempDirection = WorldPos;
        var dir = tempDirection - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
        return Quaternion.AngleAxis(angle, Vector3.forward);
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
