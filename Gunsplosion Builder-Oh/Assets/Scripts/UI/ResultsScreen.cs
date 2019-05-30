using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour
{
    public Text scoreText;

    private void Start() {
        if (GameManager.instance) {
            scoreText.text = GameManager.instance.score.ToString();
        }
    }

    public void Return() {
        SceneManager.LoadScene(0);
    }

    public void Replay() {
        GameManager.instance.ReplayLevel();
    }
}
