using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : BlockResetBase
{
    public GameObject playerPrefab;
    private GameObject spawnedPlayer;
    public SpriteRenderer sprite;

    public void StartGame() {
        spawnedPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        sprite.enabled = false;
    }

    public void ResetToEdit()
    {
        sprite.enabled = true;
        if (spawnedPlayer)
            Destroy(spawnedPlayer);
    }
}
