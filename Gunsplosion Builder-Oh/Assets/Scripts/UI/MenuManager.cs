using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Canvas pauseCanvas;
    public InputField levelName;

    public void Pause() {
        levelName.text = SaveAndLoad.instance.levelName;
        pauseCanvas.enabled = true;
    }
}
