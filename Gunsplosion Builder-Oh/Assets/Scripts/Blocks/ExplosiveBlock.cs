using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExplosiveBlock : BlockResetBase
{
    public bool exploded;
    public bool goBoom;
    public SpriteRenderer sprite;
    public Collider2D collider;

    public void StartGame()
    {
        exploded = false;
        sprite.enabled = true;
        collider.enabled = true;
    }

    public void ResetToEdit()
    {
        StartGame();
    }

    private void Update() {
        if (goBoom)
        {
            StartExplode();
            goBoom = false;
        }
    }

    public void StartExplode() {
        if (!exploded) {
            exploded = true;
            StartCoroutine(DelayedExplosion());
        }
    }

    public void Explode() {
        RaycastHit2D[] hits = new RaycastHit2D[4];
        hits[0] = Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, 0.01f);
        hits[1] = Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, 0.01f);
        hits[2] = Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, 0.01f);
        hits[3] = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, 0.01f);

        foreach (RaycastHit2D hit in hits) {
            if (hit) {
                ExplosiveBlock explosive = hit.transform.GetComponent<ExplosiveBlock>();
                if (explosive) {
                    explosive.StartExplode();
                }
            }
        }

        sprite.enabled = false;
        collider.enabled = false;
    }

    IEnumerator DelayedExplosion() {
        yield return new WaitForSeconds(0.05f);
        Explode();
    }
}
