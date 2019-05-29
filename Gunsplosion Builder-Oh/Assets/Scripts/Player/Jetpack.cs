using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public float            maxJetpackFuel,
                            jetPackForce,
                            rechargeSpeed;

    private float           jetpackFuel;
    private bool            canJetPack,
                            rechargingJetPack;

    private void Start()
    {
        canJetPack = true;
        rechargingJetPack = false;
        jetpackFuel = maxJetpackFuel;
    }

    private void FixedUpdate()
    {
        Recharge();
    }

    /////////////////////////////////////////////////////////
    // Countdown jetpack duration
    ////////////////////////////////////////////////////////
    public void Fire()
    {
        if (canJetPack &&
            jetpackFuel > 0.0f)
        { 
            jetpackFuel -= Time.deltaTime;

            if (jetpackFuel < 0.0f)
            {
                jetpackFuel = 0.0f;
                canJetPack = false;
            }
        }
    }

    public void StartRecharging()
    {
        rechargingJetPack = true;
    }

    /////////////////////////////////////////////////////////
    // Increase jetpack duration
    ////////////////////////////////////////////////////////
    private void Recharge()
    {
        if (rechargingJetPack)
        {
            // Recharge jetpack
            canJetPack = false;
            jetpackFuel += Time.deltaTime * rechargeSpeed;

            // Stop recharging
            if (jetpackFuel > maxJetpackFuel)
            {
                jetpackFuel = maxJetpackFuel;

                rechargingJetPack = false;
                canJetPack = true;
            }
        }
    }

    public bool CanJetPack()
    {
        return canJetPack;
    }

    /////////////////////////////////////////////////////////
    // Returns jetpack charge
    ////////////////////////////////////////////////////////
    public float GetFuel()
    {
        return jetpackFuel;
    }

    public float GetForce()
    {
        return jetPackForce;
    }
}
