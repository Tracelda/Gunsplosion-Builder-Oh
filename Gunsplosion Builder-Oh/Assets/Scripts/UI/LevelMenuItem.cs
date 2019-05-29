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
        GameManager.instance.gameMode = GameManager.GameModes.Play;
        GameManager.instance.PlayLevel(levelName);
    }

    public void EditLevel() {
        GameManager.instance.gameMode = GameManager.GameModes.Edit;
        GameManager.instance.LoadLevel(levelName);
    }
}
