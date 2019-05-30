using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePauseMenu : MonoBehaviour
{
    private Canvas thisCanvas;
    private bool paused;

    private void Start()
    {
        thisCanvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SetPause(!paused);
        }
    }

    public void SetPause(bool val)
    {
        paused = val;
        if (paused)
        {
            Time.timeScale = 0;
            thisCanvas.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            thisCanvas.enabled = false;
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Restart()
    {
        MenuManager.instance.Edit();
        MenuManager.instance.Play();
        SetPause(false);
    }
}
