using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image powerupImage, weapon1Image, weapon2Image;
    public Bar healthBar, armourBar, energyBar;
    public Text scoreText, multiplierText;
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

    public void SetHealth(float val) {
        healthBar.SetMeter(val);
    }

    public void SetArmour(float val) {
        armourBar.SetMeter(val);
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
}
