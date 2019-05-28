using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public InputField input;
    public Canvas thisCanvas;

    public void Save() {
        if (input.text.Length > 0) {
            SaveAndLoad.instance.SaveLevel(SaveAndLoad.instance.levelData, input.text);
            thisCanvas.enabled = false;
        }
    }

    public void Close() {
        thisCanvas.enabled = false;
    }

    public void Quit() {
        SceneManager.LoadScene(0);
    }
}
