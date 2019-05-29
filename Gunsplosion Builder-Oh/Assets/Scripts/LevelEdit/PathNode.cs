using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : BlockResetBase
{
    private SpriteRenderer sprite;

    protected override void Start()
    {
        base.Start();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void StartGame()
    {
        sprite.enabled = false;
    }

    public void ResetToEdit()
    {
        sprite.enabled = true;
    }
}
