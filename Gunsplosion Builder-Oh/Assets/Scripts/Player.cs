using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving_Entity
{
    private void FixedUpdate()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        Move(Input.GetAxis("Horizontal"), moveSpeed);

        if (Input.GetButtonDown("Jump"))
            Jump();
    }
}
