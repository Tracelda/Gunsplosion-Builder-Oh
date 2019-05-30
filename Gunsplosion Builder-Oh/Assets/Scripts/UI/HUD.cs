using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image powerupImage, weapon1Image, weapon2Image;
    public Bar healthBar, armourBar, energyBar;
    public Text scoreText, multiplierText;
    public HealthPips healthPips, armourPips;
    public static HUD instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public void SetPowerup(Sprite image) {
        powerupImage.sprite = image;
    }

    public void SetWeapon1(Sprite image) {
        weapon1Image.sprite = image;
    }

    public void SetWeapon2(Sprite image) {
        weapon2Image.sprite = image;
    }

    public void SetHealth(int val) {
        //healthBar.SetMeter(val);
        healthPips.UpdateHealth(val);
    }

    public void SetArmour(int val) {
        //armourBar.SetMeter(val);
        armourPips.UpdateHealth(val);
    }

    public void SetEnergy(float val) {
        energyBar.SetMeter(val);
    }

    public void SetScore(int val) {
        scoreText.text = val.ToString();
    }

    public void SetMultiplier(int val) {
        scoreText.text = "x" + val.ToString();
    }

    public void EnableEnergyBar()
    {
        armourPips.gameObject.SetActive(false);
        energyBar.gameObject.SetActive(true);
    }

    public void EnableArmourPips()
    {
        energyBar.gameObject.SetActive(false);
        armourPips.gameObject.SetActive(true);
    }
}
