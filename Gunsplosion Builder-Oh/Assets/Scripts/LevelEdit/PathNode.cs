using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : BlockResetBase
{
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void StartGame()
    {
        sprite.enabled = false;
    }

    public void ResetToLevel()
    {
        sprite.enabled = true;
    }
}
