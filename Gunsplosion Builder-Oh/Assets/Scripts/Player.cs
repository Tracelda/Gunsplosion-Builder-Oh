using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving_Entity
{
    public float            jumpDuration,
                            jetPackDuration,
                            jetPackForce,
                            rechargeSpeed,
                            groundPoundDuration,
                            groundPoundForce;
    public GameObject       groundPoundCollider;

    private float           currentJumpDuration,
                            currentJetPackDuration,
                            currentGroundPoundDuration;
    private bool            jetPacking,
                            canJetPack,
                            rechargingJetPack,
                            canInput,
                            groundPounding;
    private Animator        playerAnimator;
    private SpriteRenderer  playerSprite;

    private void Start()
    {
        base.Start();

        canInput = true;
        canJetPack = true;

        currentGroundPoundDuration = 0.0f;
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        PlayerInput();
        RechargeJetPack();
        UpdateAnimation();
        GroundPound();
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

            if (CanJump())
            {
                if (Input.GetAxis("Horizontal") < 0)
                {
                    playerSprite.flipX = true;
                }
                else if (Input.GetAxis("Horizontal") > 0)
                {
                    playerSprite.flipX = false;
                }
            }

            if (!CanJump() &&
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
                groundPounding = true;
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
        if (canJetPack &&
            currentJetPackDuration < jetPackDuration)
        {
            // Fire upwards
            rb.AddForce(Vector2.up * jetPackForce);

            // Countdown jetpack duration
            currentJetPackDuration += Time.deltaTime;
        }
    }

    /////////////////////////////////////////////////////////
    // Increase jetpack duration
    ////////////////////////////////////////////////////////
    private void RechargeJetPack()
    {
        if (CanJump() &&
            currentJetPackDuration >= jetPackDuration)
            rechargingJetPack = true;

        if (rechargingJetPack &&
            currentJetPackDuration > 0.0f)
        {
            // Recharge jetpack
            canJetPack = false;
            currentJetPackDuration -= Time.deltaTime * rechargeSpeed;

            // Stop recharging
            if (currentJetPackDuration < 0.0f)
            {
                currentJetPackDuration = 0.0f;

                rechargingJetPack = false;
                canJetPack = true;
            }
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
            // FALLING
            else
            {
                rb.AddForce(-Vector2.up * groundPoundForce);
            }
        }
    }

    /////////////////////////////////////////////////////////
    // Returns jetpack charge
    ////////////////////////////////////////////////////////
    public float GetJetPackFuel()
    {
        return currentJetPackDuration;
    }
    
    /////////////////////////////////////////////////////////
    // Updates sprite animation
    ////////////////////////////////////////////////////////
    private void UpdateAnimation()
    {
        playerAnimator.SetBool("InAir", !CanJump());
        playerAnimator.SetBool("Moving", rb.velocity.x != 0);
    }
}
