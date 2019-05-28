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
    }

    public weaponStats Straight, Bounce, Homing;

    [System.Serializable]
    public struct bulletType
    {
        public Weapon.weaponType weaponType;
        public float damage;
        public float bulletSpeed;
        public Sprite bulletSprite;
        public float lifeSpan;
    }
    public bulletType StraightShot, BounceShot, HomingShot;

    public GameObject bulletPrefab;
    private List<GameObject> bulletList = new List<GameObject>();
    public int bulletPoolSize;
    // Start is called before the first frame update
    void Start()
    {
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
                bulletList[i].GetComponent<Bullet>().activate(StraightShot, Vector2.zero);
                break;
           }
        }
    }
}
