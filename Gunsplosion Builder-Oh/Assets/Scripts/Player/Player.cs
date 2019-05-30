using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving_Entity
{
    public float                        jumpDuration,
                                        groundPoundDuration,
                                        groundPoundForce,
                                        speedBoostMultiplier,
                                        maxBoostCharge,
                                        boostRechargeSpeed;
    public int                          maxShieldDurability;
    public GameObject                   groundPoundCollider,
                                        jetSprite,
                                        speedSprite;
    public Animator                     playerAnimator,
                                        accessoryAnimator;
    public RuntimeAnimatorController    shieldAnim,
                                        jetpackAnim,
                                        speedBoostAnim;

    private float                       currentJumpDuration,
                                        currentGroundPoundDuration,
                                        boostCharge;
    private int                         shieldDurability;
    public bool                         jetPacking,
                                        canInput,
                                        groundPounding,
                                        shieldActive,
                                        boosting,
                                        boostRecharging;
    private SpriteRenderer              playerSprite;
    private Jetpack                     jetPack;
    public Abilities                   ability;
    private HUD                         playerHUD;

    private void Start()
    {
        base.Start();

        canInput = true;
        boostRecharging = false;
        boosting = false;

        currentGroundPoundDuration = 0.0f;
        boostCharge = maxBoostCharge;

        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        ability = GetComponent<Abilities>();
        jetPack = GetComponent<Jetpack>();
        playerHUD = HUD.instance;
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
                playerHUD.SetArmour(boostCharge);
                break;
            default:
                break;
        }

        if (CanJump())
            jetPack.StartRecharging();

        if (boosting)
        {
            currentMoveSpeed = moveSpeed * speedBoostMultiplier;

            if (!CanJump())
                boosting = false;
        }
        else
            currentMoveSpeed = moveSpeed;

        SpeedBoost();

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

            if (CanJump()) {
                if (Input.GetAxis("Horizontal") < 0) {
                    playerSprite.flipX = true;
                    accessoryAnimator.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (Input.GetAxis("Horizontal") > 0) {
                    playerSprite.flipX = false;
                    accessoryAnimator.transform.localScale = new Vector3(1, 1, 1);
                }
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
            else if (!boosting &&
                    Input.GetButtonDown("Jump") &&
                    CanJump())
            {
                Jump();
                currentJumpDuration = 0.0f;
            }

            else if (ability.type == Abilities.abilityType.JETPACK &&
                !Input.GetButton("Jump")) {
                jetSprite.SetActive(false);
            }

            // Ground pounding
            if (!CanJump() &&
                Input.GetAxis("Vertical") < 0.0f &&
                ability.type == Abilities.abilityType.JETPACK)
            {
                groundPounding = true;
                rb.AddForce(-Vector2.up * groundPoundForce);
            }

            // Speed boosting
            if (ability.type == Abilities.abilityType.SPEEDBOOST &&
                !boostRecharging &&
                Input.GetButton("Speed Boost")) {
                boosting = true;
                speedSprite.SetActive(true);
                invincible = true;
                entityHealth.takeDamage(1);
            }
            else {
                boosting = false;
                speedSprite.SetActive(false);
                invincible = false;
            }

            if (CanJump())
            {
                Aim(Input.GetAxis("Horizontal"), 0, false);
            }

            // Aim gun
            Aim(Input.GetAxis("Aim X"), Input.GetAxis("Aim Y"), !boosting);
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
            if (Input.GetButton("Jump"))
                jetSprite.SetActive(true);
            else
                jetSprite.SetActive(false);
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
        if (accessoryAnimator.runtimeAnimatorController) {
            accessoryAnimator.SetBool("InAir", !CanJump());
            accessoryAnimator.SetBool("Moving", rb.velocity.x != 0);
        }
    }

    private void SpeedBoost()
    {
        if (boosting &&
            rb.velocity.x != 0.0f)
        {
            boostCharge -= Time.deltaTime;

            if (boostCharge <= 0.0f)
            {
                boosting = false;
                boostRecharging = true;
            }
        }
        else if (boostCharge < maxBoostCharge)
        {
            boostCharge += Time.deltaTime * boostRechargeSpeed;

            if (boostCharge >= maxBoostCharge)
            {
                boostCharge = maxBoostCharge;
                boostRecharging = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            if (ability.type == Abilities.abilityType.SHIELD &&
                shieldActive)
            {
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

    public void ArmourDamage(int damage)
    {
        if (!invincible && ability.type == Abilities.abilityType.SHIELD && shieldActive) {
            shieldDurability -= damage;

            if (shieldDurability <= 0) {
                shieldActive = false;
                entityHealth.health += shieldDurability;
                accessoryAnimator.gameObject.SetActive(false);
                shieldDurability = 0;
                EffectManager.instance.PlaceParticle(transform.position, EffectManager.ParticleTypes.ArmourDestroy);
            }
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (invincible && collision.gameObject.CompareTag("Enemy")) {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth) {
                enemyHealth.takeDamage(1);
            }
        }
    }
}
