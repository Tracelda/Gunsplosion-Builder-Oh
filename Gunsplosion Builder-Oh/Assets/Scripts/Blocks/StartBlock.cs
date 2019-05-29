using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : BlockResetBase
{
    public GameObject playerPrefab;
    private GameObject spawnedPlayer;

    public void StartGame() {
        spawnedPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }

    public void ResetToEdit()
    {
        if (spawnedPlayer)
            Destroy(spawnedPlayer);
    }
}
