using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> levelObjects = new List<GameObject>();
    private bool levelLoaded;
    public enum GameModes { Play, Edit };
    public GameModes gameMode;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void NewLevel() {
        gameMode = GameModes.Edit;
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(string levelName) {
        levelLoaded = false;
        SceneManager.LoadScene(1);
        StartCoroutine(WaitLoadLevel(levelName));
    }

    IEnumerator WaitLoadLevel(string levelName) {
        yield return new WaitForFixedUpdate();
        SaveAndLoad.instance.LoadLevel(levelName);
        levelLoaded = true;
    }

    public void PlayLevel()
    {
        LevelEditor.instance.SelectBlock(0);
        foreach(GameObject obj in levelObjects)
        {
            InvokeFunction("StartGame", obj);
        }
    }

    public void PlayLevel(string levelName)
    {
        StartCoroutine(WaitPlayLevel(levelName));
    }
    IEnumerator WaitPlayLevel(string levelName)
    {
        LoadLevel(levelName);
        while (!levelLoaded)
        {
            yield return new WaitForEndOfFrame();
        }
        FindObjectOfType<MenuManager>().Play();
    }

    public void EditLevel()
    {
        foreach (GameObject obj in levelObjects)
        {
            if (obj)
            {
                InvokeFunction("ResetToEdit", obj);
            }
        }
    }

    // Run functions on game object
    void InvokeFunction(string function, GameObject obj)
    {
        foreach (MonoBehaviour component in obj.GetComponents<MonoBehaviour>())
        {
            if (component.GetType().GetMethod(function) != null)
            {
                component.Invoke(function, 0f);
            }
        }
    }
}
