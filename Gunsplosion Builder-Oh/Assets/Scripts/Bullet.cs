﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon.BulletType currentType;
    public bool active;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float lifeSpan;
    private BoxCollider2D boxCollider;

    //bounce logic
    private float bounceCooldown;
    private float maxBounceCoolDown;
    private bool bounceOnCooldown;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        active = false;

        bounceCooldown = 0.2f;
        maxBounceCoolDown = bounceCooldown;
        bounceOnCooldown = false;
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
        setColliderActiveState(true);
        //GetComponent<PolygonCollider2D>().
        //gameObject.transform.localRotation = ;
    }
    public void deActivate()
    {
        active = false;
        spriteRenderer.sprite = null;
        rb.velocity = Vector2.zero;
        setColliderActiveState(false);
    }
    public void setColliderActiveState(bool input)
    {
        boxCollider.enabled = input;
    }

    private void straightShot()
    {
        rb.velocity = transform.up * currentType.bulletSpeed;
    }
    private void homingShot()
    {
        rb.velocity = transform.up * currentType.bulletSpeed;
        if (!bounceOnCooldown)
        {
            int layer_mask = LayerMask.GetMask("Enemy");
            var hit = Physics2D.CircleCast(transform.position, currentType.homingRaduis, Vector2.up,0,layer_mask);
            //print(hit.collider);
            if (hit)
            {
                if (hit.collider.gameObject)
                {
                    if (hit.collider.gameObject.GetComponent<Health>())
                    {
                        Quaternion toRotation = lookAt2D(hit.point);
                        //transform.rotation = toRotation;
                        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, currentType.homingSpeed * Time.deltaTime);
                        bounceOnCooldown = true;
                    }
                }
            }
        }
        else
        {
            bounceCooldown -= Time.deltaTime;
            if(bounceCooldown<0)
            {
                bounceOnCooldown = false;
                bounceCooldown = maxBounceCoolDown;
            }
        }
    }
    private void bounceShot()
    {
        rb.velocity = transform.up * currentType.bulletSpeed;
        int layer_mask = LayerMask.GetMask("Block");
        var hit = Physics2D.Raycast(transform.position, transform.up, Time.deltaTime * (currentType.bulletSpeed*2), layer_mask);
        if(hit)
        {
            Vector2 reflect = Vector2.Reflect(transform.up, hit.normal);
            float rot =  Mathf.Atan2(reflect.y, reflect.x) * Mathf.Rad2Deg -90f;
            transform.eulerAngles = new Vector3(0,0, rot);

            //transform.rotation = lookAt2D(reflect);
        }
    }

    private Quaternion lookAt2D(Vector2 WorldPos)
    {
        Vector3 tempDirection = WorldPos;
        var dir = tempDirection - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active)
        {
            if (other.gameObject)
            {
                if (other.gameObject.GetComponent<Health>())
                {
                    if (other.gameObject.CompareTag(currentType.damageTag))
                    {
                        print("we are hurting");
                        other.gameObject.GetComponent<Health>().takeDamage(currentType.damage);
                        deActivate();
                    }
                }
            }
        }
    }
}
