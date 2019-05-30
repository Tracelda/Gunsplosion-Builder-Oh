using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : BlockResetBase
{
    private SpriteRenderer sprite;
    private Vector3 basePosition;
    private bool inGame;

    protected override void Start()
    {
        base.Start();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void StartGame()
    {
        //sprite.enabled = false;
        basePosition = transform.position;
        inGame = true;

    }

    public void ResetToEdit()
    {
        sprite.enabled = true;
        inGame = false;
    }

    private void Update() {
        if (inGame) {
            transform.position = basePosition;
        }
    }
}
