using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public Abilities.abilityType            ability;
    public List<RuntimeAnimatorController>  listofSprites = new List<RuntimeAnimatorController>();
    public Animator                         sprite;
    public GameObject                       spriteTooltip;

    private Abilities.abilityType           temp;

    private void Update()
    {
        switch (ability)
        {
            case Abilities.abilityType.NONE:
                gameObject.SetActive(false);
                break;
            case Abilities.abilityType.JETPACK:
                sprite.runtimeAnimatorController = listofSprites[0];
                break;
            case Abilities.abilityType.SHIELD:
                sprite.runtimeAnimatorController = listofSprites[1];
                break;
            case Abilities.abilityType.SPEEDBOOST:
                sprite.runtimeAnimatorController = listofSprites[2];
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject &&
            collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Pickup"))
            {
                SwapAbilities(collision);
            }

            spriteTooltip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject &&
            collision.gameObject.CompareTag("Player"))
        {
            spriteTooltip.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject &&
            collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Pickup"))
            {
                SwapAbilities(collision);
            }
        }
    }

    private void SwapAbilities(Collider2D collision)
    {
        temp = collision.GetComponent<Abilities>().type;

        switch (ability)
        {
            case Abilities.abilityType.JETPACK:
                collision.GetComponent<Player>().EnableJetPack();
                break;
            case Abilities.abilityType.SHIELD:
                collision.GetComponent<Player>().EnableShield();
                break;
            case Abilities.abilityType.SPEEDBOOST:
                collision.GetComponent<Player>().EnableSpeedBoost();
                break;
            default:
                break;
        }

        ability = temp;
    }
}
