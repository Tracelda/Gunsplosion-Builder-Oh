using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving_Entity
{
    public float                        jumpDuration,
                                        groundPoundDuration,
                                        groundPoundForce;
    public int                          maxShieldDurability;
    public GameObject                   groundPoundCollider;
    public Animator                     playerAnimator,
                                        accessoryAnimator;
    public RuntimeAnimatorController    shieldAnim,
                                        jetpackAnim,
                                        speedBoostAnim;
    public HUD                          playerHUD;

    private float                       currentJumpDuration,
                                        currentGroundPoundDuration;
    private int                         shieldDurability;
    private bool                        jetPacking,
                                        canInput,
                                        groundPounding,
                                        shieldActive;
    private SpriteRenderer              playerSprite;
    private Jetpack                     jetPack;
    private Abilities                   ability;

    private void Start()
    {
        base.Start();

        canInput = true;

        currentGroundPoundDuration = 0.0f;
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        ability = GetComponent<Abilities>();
        jetPack = GetComponent<Jetpack>();
    }

    private void FixedUpdate()
    {
        PlayerInput();
        UpdateAnimation();
        GroundPound();

        playerHUD.SetHealth((int)entityHealth.health);

        switch (ability.type)
        {
            case Abilities.abilityType.JETPACK:
                playerHUD.SetArmour(jetPack.GetFuel());
                break;
            case Abilities.abilityType.SHIELD:
                playerHUD.SetArmour((float)shieldDurability / (float)maxShieldDurability);
                break;
            case Abilities.abilityType.SPEEDBOOST:
                break;
            default:
                break;
        }

        if (CanJump())
            jetPack.StartRecharging();

    }
    
    /////////////////////////////////////////////////////////
    // Runs functions from input
    ////////////////////////////////////////////////////////
    private void PlayerInput()
    {
        if (canInput)
        {
            // Move left abd right
            Move(Input.GetAxis("Horizontal"));

            if (Input.GetAxis("Horizontal") < 0)
            {
                playerSprite.flipX = true;
                accessoryAnimator.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                playerSprite.flipX = false;
                accessoryAnimator.transform.localScale = new Vector3(1, 1, 1);
            }

            if (ability.type == Abilities.abilityType.JETPACK &&
                !CanJump() &&
                !jetPacking)
            {
                // Timer for jump duration
                if (currentJumpDuration < jumpDuration)
                    currentJumpDuration += Time.deltaTime;

                // Jetpack
                else if (Input.GetButton("Jump"))
                {
                    FireJetPack();
                }
            }

            // Jump
            else if (Input.GetButtonDown("Jump") &&
                    CanJump())
            {
                Jump();
                currentJumpDuration = 0.0f;
            }

            // Ground pounding
            if (!CanJump() &&
                Input.GetAxis("Vertical") < 0.0f &&
                ability.type == Abilities.abilityType.JETPACK)
            {
                groundPounding = true;
                rb.AddForce(-Vector2.up * groundPoundForce);
            }

            // Aim gun
            if (CanJump())
            {
                Aim(Input.GetAxis("Horizontal"), 0, false);
            }
            Aim(Input.GetAxis("Aim X"), Input.GetAxis("Aim Y"));
        }

        // Switch weapon
        if (Input.GetButtonDown("Switch Weapon"))
            weaponScript.swapActiveWeapon();
    }

    /////////////////////////////////////////////////////////
    // Boost upwards
    ////////////////////////////////////////////////////////
    private void FireJetPack()
    {
        if (jetPack.CanJetPack())
        {
            // Fire upwards
            rb.AddForce(Vector2.up * jetPack.GetForce());
            jetPack.Fire();
        }
    }

    /////////////////////////////////////////////////////////
    // Drop down and ground pound
    ////////////////////////////////////////////////////////
    private void GroundPound()
    {
        if (groundPounding)
        {
            // HIT
            if (CanJump())
            {
                groundPoundCollider.SetActive(true);

                if (currentGroundPoundDuration < groundPoundDuration)
                    currentGroundPoundDuration += Time.deltaTime;

                else
                {
                    groundPoundCollider.SetActive(false);

                    currentGroundPoundDuration = 0.0f;

                    groundPounding = false;
                    canInput = true;
                }
            }
        }
    }
    
    /////////////////////////////////////////////////////////
    // Updates sprite animation
    ////////////////////////////////////////////////////////
    private void UpdateAnimation()
    {
        playerAnimator.SetBool("InAir", !CanJump());
        playerAnimator.SetBool("Moving", rb.velocity.x != 0);
        accessoryAnimator.SetBool("InAir", !CanJump());
        accessoryAnimator.SetBool("Moving", rb.velocity.x != 0);
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            if (ability.type == Abilities.abilityType.SHIELD &&
                shieldActive)
            {
                Debug.Log(true);
                shieldDurability -= damage;

                if (shieldDurability <= 0)
                {
                    shieldActive = false;
                    entityHealth.health += shieldDurability;
                    accessoryAnimator.gameObject.SetActive(false);
                    shieldDurability = 0;
                    EffectManager.instance.PlaceParticle(transform.position, EffectManager.ParticleTypes.ArmourDestroy);
                }
            }
            else
            {
                entityHealth.health -= damage;
            }

            if (entityHealth.health < 0)
                entityHealth.health = 0;
        }
    }

    public void EnableShield()
    {
        ability.type = Abilities.abilityType.SHIELD;
        shieldActive = true;
        accessoryAnimator.runtimeAnimatorController = shieldAnim;
        accessoryAnimator.enabled = true;

        shieldDurability = maxShieldDurability;

        playerHUD.EnableArmourPips();
    }

    public void EnableJetPack()
    {
        ability.type = Abilities.abilityType.JETPACK;
        shieldActive = false;
        accessoryAnimator.runtimeAnimatorController = jetpackAnim;
        accessoryAnimator.enabled = true;

        playerHUD.EnableArmourPips();
    }

    public void EnableSpeedBoost()
    {
        ability.type = Abilities.abilityType.SPEEDBOOST;
        shieldActive = false;
        accessoryAnimator.runtimeAnimatorController = speedBoostAnim;
        accessoryAnimator.enabled = true;

        playerHUD.EnableArmourPips();
    }

    public void DisableAbility()
    {
        ability.type = Abilities.abilityType.NONE;
        shieldActive = false;
        accessoryAnimator.runtimeAnimatorController = null;
        accessoryAnimator.enabled = false;

        playerHUD.DisableExtraBar();
    }
}
