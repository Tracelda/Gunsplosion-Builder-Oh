using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : BlockResetBase
{
    private SpriteRenderer sprite;
    private Vector3 basePosition;
    private bool inGame;
    private LineRenderer line;

    protected override void Start()
    {
        base.Start();
        sprite = GetComponent<SpriteRenderer>();
        line = GetComponent<LineRenderer>();
    }

    public void StartGame()
    {
        sprite.enabled = false;
        basePosition = transform.position;
        inGame = true;
        line.enabled = false;
    }

    public void ResetToEdit()
    {
        sprite.enabled = true;
        inGame = false;
        line.enabled = true;
        transform.position = basePosition;
    }

    private void Update() {
        if (inGame) {
            transform.position = basePosition;
        }
    }
}
