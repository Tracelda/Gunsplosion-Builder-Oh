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
    private BoxCollider2D boxCollider;
    private Animator animator;
    private AudioSource audioSource;

    //bounce logic
    private float bounceCooldown;
    private float maxBounceCoolDown;
    private bool bounceOnCooldown;

    // homing logic
    private Transform homingTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        active = false;
        audioSource = GetComponent<AudioSource>();

        bounceCooldown = 0.2f;
        maxBounceCoolDown = bounceCooldown;
        bounceOnCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
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
                case Weapon.weaponType.shotgun:
                    straightShot();
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
        if (currentType.weaponType == Weapon.weaponType.shotgun)
        {
            float rand = Random.Range(-currentType.shotGunSpread, currentType.shotGunSpread);
            print(rand);
            transform.rotation = lookAt2D(aimDirection,rand);
        }
        else
        {
            transform.rotation = lookAt2D(aimDirection);// + Quaternion.Euler(new Vector3(0,Random.Range(-5,5),0));
        }
        if (currentType.weaponType != Weapon.weaponType.Homing)
            animator.runtimeAnimatorController = currentType.animatorController;
        setColliderActiveState(true);
        //GetComponent<PolygonCollider2D>().
        //gameObject.transform.localRotation = ;
    }
    public void deActivate()
    {
        active = false;
        spriteRenderer.sprite = null;
        rb.velocity = Vector2.zero;
        animator.runtimeAnimatorController = null;
        setColliderActiveState(false);
        homingTarget = null;
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
        //if (!bounceOnCooldown)
        //{
        int layer_mask = LayerMask.GetMask("Enemy");
        //var multiHit = Physics2D.CircleCastAll(transform.position, currentType.homingRaduis, Vector2.zero);
        var multiHit = Physics2D.OverlapCircleAll(transform.position, currentType.homingRaduis);

        if (!homingTarget) {
            foreach (Collider2D hit in multiHit) {
                if (hit) {
                    if (hit.transform.CompareTag(currentType.damageTag)) {
                        homingTarget = hit.transform;
                    }
                }
            }
        }

        if (homingTarget) {
            Quaternion toRotation = lookAt2D(homingTarget.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, currentType.homingSpeed * Time.deltaTime);
            bounceOnCooldown = true;
        }

    }
    private void bounceShot()
    {
        rb.velocity = transform.up * currentType.bulletSpeed;
        int layer_mask = LayerMask.GetMask("Block");
        var hit = Physics2D.Raycast(transform.position, transform.up, Time.deltaTime * (currentType.bulletSpeed*1.5f), layer_mask);
        if(hit)
        {
            Vector2 reflect = Vector2.Reflect(transform.up, hit.normal);
            float rot =  Mathf.Atan2(reflect.y, reflect.x) * Mathf.Rad2Deg -90f;
            transform.eulerAngles = new Vector3(0,0, rot);
            audioSource.PlayOneShot(currentType.bulletSound, 0.5f);
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
    private Quaternion lookAt2D(Vector2 WorldPos, float shotSpread)
    {
        Vector3 tempDirection = WorldPos;
        var dir = tempDirection - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x ) * Mathf.Rad2Deg - 90 + shotSpread;
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
                        EffectManager.instance.PlaceParticle(transform.position, EffectManager.ParticleTypes.SmallExplosion);
                        other.gameObject.GetComponent<Health>().takeDamage(currentType.damage);
                        deActivate();
                    }
                }
                else if(currentType.weaponType != Weapon.weaponType.Bounce)
                {
                    if(other.gameObject.CompareTag("Block"))
                    {
                        EffectManager.instance.PlaceParticle(transform.position, EffectManager.ParticleTypes.SmallExplosion);
                        deActivate();
                    }
                }
            }
        }
    }
}
