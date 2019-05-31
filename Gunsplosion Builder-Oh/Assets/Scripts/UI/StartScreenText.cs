using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenText : MonoBehaviour
{
    Text text;
    private float timer;

    private void Start() {
        text = GetComponent<Text>();
    }

    private void Update() {
        timer -= Time.deltaTime;

        if (timer <= 0) {
            text.enabled = !text.enabled;
            timer = 0.5f;
        }

        if (Input.anyKeyDown) {
            SceneManager.LoadScene(1);
        }
    }
}
