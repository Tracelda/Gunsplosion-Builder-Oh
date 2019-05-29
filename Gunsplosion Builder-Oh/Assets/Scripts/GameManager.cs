using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> levelObjects = new List<GameObject>();

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);
    }

    public void NewLevel() {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(1);
        StartCoroutine(WaitLoadLevel(levelName));
    }

    IEnumerator WaitLoadLevel(string levelName) {
        yield return new WaitForFixedUpdate();
        SaveAndLoad.instance.LoadLevel(levelName);
    }

    public void PlayLevel()
    {
        foreach(GameObject obj in levelObjects)
        {
            InvokeFunction("StartGame", obj);
        }
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
