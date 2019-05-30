using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float iframeDuration;
    private float iframes;
    private Color baseColour;
    private SpriteRenderer sprite;
    private int iframeFrequency = 4;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite)
        {
            baseColour = sprite.color;
        }
    }

    void Update()
    {
        iframes -= Time.deltaTime;
        if (iframes > 0 && sprite && Time.frameCount % iframeFrequency == 0)
        {
            sprite.color = Color.red;
        }
        else if (iframes <= 0 && sprite.color != baseColour)
        {
            sprite.color = baseColour;
        }
        else
        {
            sprite.color = baseColour;
        }
    }

    public void takeDamage(float damage)
    {
        if (iframes <= 0)
        {
            health -= damage;
            iframes = iframeDuration;
        }
        //print(health);
    }
}
