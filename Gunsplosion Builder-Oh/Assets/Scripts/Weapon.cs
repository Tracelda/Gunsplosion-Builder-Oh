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
        public AudioClip shotSound;
    }
    [System.Serializable]
    public struct BulletType
    {
        public Weapon.weaponType weaponType;
        public float damage;
        public float bulletSpeed;
        public Sprite bulletSprite;
        public RuntimeAnimatorController animatorController;
        public float lifeSpan;
        public float homingRaduis;
        public float homingSpeed;
        public string damageTag;
        public AudioClip bulletSound;
    }
    public weaponStats Straight, Bounce, Homing;
    public BulletType StraightShot, BounceShot, HomingShot;

    weaponStats[] heldWeapons = new weaponStats[2]; 

    public GameObject bulletPrefab;
    private List<GameObject> bulletList = new List<GameObject>();
    public int bulletPoolSize;

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer gunSprite;
    public GameObject aimDirection;
    public string OnlyDamageTag;
    private AudioSource audioSource;


    private List<weaponStats> allWeapons = new List<weaponStats>();
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Straight.bulletType = StraightShot;
        Homing.bulletType = HomingShot;
        Bounce.bulletType = BounceShot;

        Straight.bulletType.damageTag = OnlyDamageTag;
        Homing.bulletType.damageTag = OnlyDamageTag;
        Bounce.bulletType.damageTag = OnlyDamageTag;

        //spriteRenderer = GetComponent<SpriteRenderer>();
        heldWeapons[0] = Bounce;
        heldWeapons[1] = Homing;

        allWeapons.Add(Straight);
        allWeapons.Add(Bounce);
        allWeapons.Add(Homing);

        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject shot = Instantiate(bulletPrefab);
            shot.GetComponent<BoxCollider2D>().enabled = false;
            bulletList.Add(shot);
            shot.transform.SetParent(transform);
            
        }

        changeWeapon(0);
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
                    //bulletList[i].GetComponent<BoxCollider2D>().enabled = false;
                    EffectManager.instance.AttachParticle(bulletList[i].transform, (EffectManager.ParticleTypes)(int)heldWeapons[0].bulletType.weaponType);
                    audioSource.PlayOneShot(heldWeapons[0].shotSound, 0.5f);
                    break;
                }
            }
        }
    }

    public int changeWeapon(int index)
    {
        int result = int.MaxValue;
        if(heldWeapons[0].type != allWeapons[index].type)
        {
            for (int i = 0; i < allWeapons.Count; i++)
            {
                if(allWeapons[i].type == heldWeapons[0].type)
                {
                    heldWeapons[0] = allWeapons[index];
                    gunSprite.sprite = heldWeapons[0].weaponSprite;
                    UpdateWeaponUI();
                    return i;
                }
            }
            
        }
        else
        {
            return int.MaxValue;
        }


        return result;
    }
    public void swapActiveWeapon()
    {
        weaponStats temp = heldWeapons[0];
        heldWeapons[0] = heldWeapons[1];
        heldWeapons[1] = temp;

        gunSprite.sprite = heldWeapons[0].weaponSprite;

        UpdateWeaponUI();
    }

    public void UpdateWeaponUI()
    {
        HUD.instance.SetWeapon1(heldWeapons[0].bulletType.bulletSprite);
        HUD.instance.SetWeapon2(heldWeapons[1].bulletType.bulletSprite);
    }
}
