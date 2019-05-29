using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlock : BlockResetBase
{
    public Rigidbody2D rb;
    private Vector3 startPosition;

    public void StartGame()
    {
        startPosition = transform.position;
        rb.simulated = true;
    }

    public void ResetToEdit()
    {
        rb.simulated = false;
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
