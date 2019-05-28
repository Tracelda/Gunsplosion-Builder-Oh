using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving_Entity
{
    public float    jumpDuration,
                    jetPackDuration,
                    rechargeSpeed;

    private float   currentJumpDuration,
                    currentJetPackDuration;

    private void FixedUpdate()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        Move(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void StartJumpDuration()
    {

    }
}
