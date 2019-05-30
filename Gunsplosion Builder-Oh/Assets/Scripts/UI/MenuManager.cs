using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Canvas pauseCanvas;
    public InputField levelName;
    public Canvas editMenu, playMenu;
    public GameObject grid;
    public Button playButton;
    public EditCamera editCamera;
    public bool isPlaying;
    public GameObject clearParticle;

    public static MenuManager instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    private void Start() {
        GameManager.instance.score = 0;
        GameManager.instance.multiplier = 1;
    }

    public void Pause() {
        if (SaveAndLoad.instance.levelName != "") {
            levelName.text = SaveAndLoad.instance.levelName;
        }
        pauseCanvas.enabled = true;
    }

    public void Play()
    {
        isPlaying = true;
        GameManager.instance.PlayLevel();
        editMenu.enabled = false;
        playMenu.enabled = true;
        playButton.enabled = false;
        grid.SetActive(false);
        StartCoroutine(ChangeCamera());
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Edit()
    {
        isPlaying = false;
        GameManager.instance.EditLevel();
        editMenu.enabled = true;
        playMenu.enabled = false;
        playButton.enabled = true;
        grid.SetActive(true);
        editCamera.ChangeToEdit();
    }

    IEnumerator ChangeCamera() {
        yield return new WaitForSeconds(0.01f);
        var player = FindObjectOfType<Player>();
        if (player)
            editCamera.ChangeToPlay(player.transform);
    }

    public GameObject ClearEffect() {
        return Instantiate(clearParticle, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f)), Quaternion.identity);
    }
}
