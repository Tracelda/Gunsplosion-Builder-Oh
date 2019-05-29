using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum weaponType { Straight, Bounce, Homing};

    [System.Serializable]
    public struct weaponStats
    {
        public weaponType type;
        public float fireRate;
        public bool active;
        public int currentSlot;

        public Sprite weaponSprite;
        public Vector3 shotStartPosition;
        public BulletType bulletType;
        public float currentShotCooldown;
    }
    [System.Serializable]
    public struct BulletType
    {
        public Weapon.weaponType weaponType;
        public float damage;
        public float bulletSpeed;
        public Sprite bulletSprite;
        public float lifeSpan;
        public float homingRaduis;
        public float homingSpeed;
        public string damageTag;
    }
    public weaponStats Straight, Bounce, Homing;
    public BulletType StraightShot, BounceShot, HomingShot;

    weaponStats[] heldWeapons = new weaponStats[2]; 

    public GameObject bulletPrefab;
    private List<GameObject> bulletList = new List<GameObject>();
    public int bulletPoolSize;

    private SpriteRenderer spriteRenderer;
    public GameObject aimDirection;
    public string OnlyDamageTag;
    // Start is called before the first frame update
    void Start()
    {
        Straight.bulletType = StraightShot;
        Homing.bulletType = HomingShot;
        Bounce.bulletType = BounceShot;

        Straight.bulletType.damageTag = OnlyDamageTag;
        Homing.bulletType.damageTag = OnlyDamageTag;
        Bounce.bulletType.damageTag = OnlyDamageTag;

        spriteRenderer = GetComponent<SpriteRenderer>();
        heldWeapons[0] = Bounce;
        heldWeapons[1] = Homing;

        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject shot = Instantiate(bulletPrefab);
            bulletList.Add(shot);
            shot.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < heldWeapons.Length; i++)
        {
            if(heldWeapons[i].currentShotCooldown>0)
            {
                heldWeapons[i].currentShotCooldown -= Time.deltaTime;
            }
        }
        
    }

    public void fire()
    {
        if (heldWeapons[0].currentShotCooldown <= 0)
        {
            for (int i = 0; i < bulletPoolSize; i++)
            {
                if (!bulletList[i].GetComponent<Bullet>().active)
                {
                    bulletList[i].GetComponent<Bullet>().activate(heldWeapons[0].bulletType,
                        heldWeapons[0].shotStartPosition, aimDirection.transform.position);
                    heldWeapons[0].currentShotCooldown = heldWeapons[0].fireRate;
                    break;
                }
            }
        }
    }

    public void changeWeapon()
    {

    }
    public void swapActiveWeapon()
    {
        weaponStats temp = heldWeapons[0];
        heldWeapons[0] = heldWeapons[1];
        heldWeapons[1] = temp;

        spriteRenderer.sprite = heldWeapons[0].weaponSprite;
    }
}
