using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBlock : MonoBehaviour
{
    public RuntimeAnimatorController depthSprite, topSprite;
    public Animator sprite;
    private List<Health> healthsInside = new List<Health>();

    private void OnTriggerEnter2D(Collider2D collision) {
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth) {
            Player player = collision.GetComponent<Player>();
            if (player && player.ability.type != Abilities.abilityType.SHIELD) {
                healthsInside.Add(playerHealth);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth) {
            Player player = collision.GetComponent<Player>();
            if (player && player.ability.type != Abilities.abilityType.SHIELD) {
                healthsInside.Remove(playerHealth);
            }
        }
    }

    private void Start() {
        SetSprite();
    }

    private void FixedUpdate() {
        if (!MenuManager.instance.isPlaying) {
            SetSprite();
        }

        foreach (Health health in healthsInside) {
            if (health) {
                health.takeDamage(1);
            }
        }
    }

    private void SetSprite() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1.2f);
        if (hit) {
            AcidBlock acid = hit.transform.GetComponent<AcidBlock>();
            if (acid) {
                sprite.runtimeAnimatorController = depthSprite;
            }
            else {
                sprite.runtimeAnimatorController = topSprite;
            }
        }
        else {
            sprite.runtimeAnimatorController = topSprite;
        }
    }
}
