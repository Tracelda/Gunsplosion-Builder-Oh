using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenText : MonoBehaviour
{
    Text text;
    private float timer;
    public Animator animator;

    private void Start() {
        text = GetComponent<Text>();
        animator.speed = 0;
    }

    private void Update() {
        timer -= Time.deltaTime;

        if (timer <= 0) {
            text.enabled = !text.enabled;
            timer = 0.5f;
        }

        if (Input.anyKeyDown) {
            StartCoroutine(CoolTitle());
        }
    }

    IEnumerator CoolTitle() {
        animator.speed = 1;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}
