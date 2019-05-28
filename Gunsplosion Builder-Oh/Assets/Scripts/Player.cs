using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving_Entity
{
    public float        jumpDuration,
                        jetPackDuration,
                        jetPackForce,
                        rechargeSpeed;

    private float       currentJumpDuration,
                        currentJetPackDuration;
    private bool        jetPacking,
                        canJetPack,
                        rechargingJetPack;

    private void Start()
    {
        base.Start();
        canJetPack = true;
    }

    private void FixedUpdate()
    {
        PlayerInput();
        RechargeJetPack();
    }

    private void PlayerInput()
    {
        Move(Input.GetAxis("Horizontal"));

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

        // Aim gun
        Aim(Input.GetAxis("Aim X"), Input.GetAxis("Aim Y"));
    }

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
}
