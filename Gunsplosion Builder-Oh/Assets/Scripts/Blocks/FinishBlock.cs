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
                    StartCoroutine(DelayedEnd());
                    break;
            }
        }
    }

    IEnumerator DelayedEnd() {
        Time.timeScale = 0.5f;
        GameObject effect = MenuManager.instance.ClearEffect();
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(2);
        effect.transform.position = Vector3.zero;
        Time.timeScale = 1;
    }
}
