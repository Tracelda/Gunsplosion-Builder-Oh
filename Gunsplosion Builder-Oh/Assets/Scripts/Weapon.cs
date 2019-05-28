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
    }
    [System.Serializable]
    public struct BulletType
    {
        public Weapon.weaponType weaponType;
        public float damage;
        public float bulletSpeed;
        public Sprite bulletSprite;
        public float lifeSpan;
    }
    public weaponStats Straight, Bounce, Homing;
    public BulletType StraightShot, BounceShot, HomingShot;

    weaponStats[] heldWeapons = new weaponStats[2]; 

    public GameObject bulletPrefab;
    private List<GameObject> bulletList = new List<GameObject>();
    public int bulletPoolSize;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Straight.bulletType = StraightShot;
        Homing.bulletType = HomingShot;
        Bounce.bulletType = BounceShot;

        spriteRenderer = GetComponent<SpriteRenderer>();
        heldWeapons[0] = Straight;
        heldWeapons[1] = Homing;

        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject shot = Instantiate(bulletPrefab);
            bulletList.Add(shot);
            shot.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fire()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
           if (!bulletList[i].GetComponent<Bullet>().active)
           {
                bulletList[i].GetComponent<Bullet>().activate(heldWeapons[0].bulletType, Vector2.zero);
                break;
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
