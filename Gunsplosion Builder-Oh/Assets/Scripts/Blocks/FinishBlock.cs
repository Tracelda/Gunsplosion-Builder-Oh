using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            switch (GameManager.instance.gameMode)
            {
                case GameManager.GameModes.Edit:
                    MenuManager.instance.Edit();
                    break;
                case GameManager.GameModes.Play:
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }
}
