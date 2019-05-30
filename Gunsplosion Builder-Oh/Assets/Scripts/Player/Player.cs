using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving_Entity
{
    public float            jumpDuration,
                            groundPoundDuration,
                            groundPoundForce,
                            maxShieldDurability;
    public GameObject       groundPoundCollider;
    public Animator         playerAnimator,
                            accessoryAnimator;

    private float           currentJumpDuration,
                            currentGroundPoundDuration,
                            shieldDurability;
    private bool            jetPacking,
                            canInput,
                            groundPounding,
                            shieldActive;
    private SpriteRenderer  playerSprite;
    private Jetpack         jetPack;
    private Abilities       ability;

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
                Input.GetAxis("Vertical") < 0.0f)
            {
                groundPounding = true;
                rb.AddForce(-Vector2.up * groundPoundForce);
            }
        }
        
        // Aim gun
        Aim(Input.GetAxis("Aim X"), Input.GetAxis("Aim Y"));

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

    public void TakeDamage(float damage)
    {
        if (ability.type == Abilities.abilityType.SHIELD &&
            shieldActive)
        {
            shieldDurability -= damage;

            if (shieldDurability <= 0.0f)
            {
                shieldActive = false;
                entityHealth.health += shieldDurability;
                accessoryAnimator.gameObject.SetActive(false);
                shieldDurability = 0.0f;
            }
        }
    }
}
