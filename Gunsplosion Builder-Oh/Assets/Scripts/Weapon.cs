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
        public Weapon.weaponType type;
        public float damage;
        public Sprite bulletShot;
        public float lifespan;
    }
    public bulletType StraightShot, BounceShot, HomingShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void fire()
    {

    }
}
