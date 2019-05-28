using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
}
