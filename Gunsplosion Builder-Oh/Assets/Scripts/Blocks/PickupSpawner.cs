using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : BlockResetBase
{
    private GameObject spawnedPickup;
    public int pickupID;
    public GameObject prefab;

    public void StartGame()
    {
        spawnedPickup = Instantiate(prefab, transform.position, Quaternion.identity);
        spawnedPickup.GetComponent<WeaponPickup>().pickupIndex = pickupID;
        spawnedPickup.GetComponent<WeaponPickup>().updateSprite();
    }

    public void ResetToEdit()
    {
        Destroy(spawnedPickup);
    }
}
