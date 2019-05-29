using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Canvas pauseCanvas;
    public InputField levelName;
    public Canvas editMenu, playMenu;
    public GameObject grid;
    public Button playButton;

    public static MenuManager instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public void Pause() {
        levelName.text = SaveAndLoad.instance.levelName;
        pauseCanvas.enabled = true;
    }

    public void Play()
    {
        GameManager.instance.PlayLevel();
        editMenu.enabled = false;
        playMenu.enabled = true;
        playButton.enabled = false;
        grid.SetActive(false);
    }

    public void Edit()
    {
        GameManager.instance.EditLevel();
        editMenu.enabled = true;
        playMenu.enabled = false;
        playButton.enabled = true;
        grid.SetActive(true);
    }
}
