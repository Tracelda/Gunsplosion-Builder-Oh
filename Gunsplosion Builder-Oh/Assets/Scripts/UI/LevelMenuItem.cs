using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuItem : MonoBehaviour
{
    public string levelName;
    public Text nameText;

    public void UpdateUI() {
        nameText.text = levelName;
    }

    public void PlayLevel() {

    }

    public void EditLevel() {
        GameManager.instance.LoadLevel(levelName);
    }
}
