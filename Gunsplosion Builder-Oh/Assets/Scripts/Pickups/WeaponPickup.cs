using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public List<Sprite> possiblePickups = new List<Sprite>();
    public int pickupIndex;
    public SpriteRenderer spriteRenderer;
    public GameObject spriteTooltip;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteTooltip.SetActive(false);
        updateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                spriteTooltip.SetActive(true);
                if (Input.GetKey(KeyCode.R))
                {
                    swapWeapon(collision);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Input.GetKey(KeyCode.R))
                {
                    swapWeapon(collision);
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                spriteTooltip.SetActive(false);
            }
        }
    }

    public void updateSprite()
    {
        spriteRenderer.sprite = possiblePickups[pickupIndex];
    }
    private void swapWeapon(Collider2D collision)
    {
        int oldIndex = pickupIndex;
        pickupIndex = collision.gameObject.GetComponentInChildren<Weapon>().changeWeapon(pickupIndex);
        if (pickupIndex != int.MaxValue)
        {
            updateSprite();
        }
        else
        {
            pickupIndex = oldIndex;
        }
    }
}
