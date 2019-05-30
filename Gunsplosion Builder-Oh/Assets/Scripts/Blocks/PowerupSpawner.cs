using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : BlockResetBase
{
    private GameObject spawnedPickup;
    public int pickupID;
    public GameObject prefab;
    public SpriteRenderer sprite;
    public Abilities.abilityType type;

    public void StartGame()
    {
        spawnedPickup = Instantiate(prefab, transform.position, Quaternion.identity);
        spawnedPickup.GetComponent<AbilityPickup>().ability = type;
        sprite.enabled = false;
    }

    public void ResetToEdit()
    {
        Destroy(spawnedPickup);
        sprite.enabled = true;
    }
}
