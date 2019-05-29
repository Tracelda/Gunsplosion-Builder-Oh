using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : BlockResetBase
{
    private GameObject spawnedPickup;
    public int pickupID;
    public GameObject prefab;
    public SpriteRenderer sprite;

    public void StartGame()
    {
        spawnedPickup = Instantiate(prefab, transform.position, Quaternion.identity);
        spawnedPickup.GetComponent<WeaponPickup>().pickupIndex = pickupID;
        spawnedPickup.GetComponent<WeaponPickup>().updateSprite();
        sprite.enabled = false;
    }

    public void ResetToEdit()
    {
        Destroy(spawnedPickup);
        sprite.enabled = true;
    }
}
