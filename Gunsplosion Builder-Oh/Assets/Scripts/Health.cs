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
    public enum EntityTypes { None, Player, Enemy };
    public EntityTypes entityType;

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

            if (health <= 0)
            {
                switch (entityType)
                {
                    case EntityTypes.Player:
                        EffectManager.instance.PlaceParticle(transform.position, EffectManager.ParticleTypes.PlayerDestroy);
                        HUD.instance.SetHealth(0);
                        GameManager.instance.Restartlater();
                        Destroy(gameObject);
                        break;
                    case EntityTypes.Enemy:
                        EffectManager.instance.PlaceParticle(transform.position, EffectManager.ParticleTypes.LargeExplosion);
                        GameManager.instance.AddMultiplier(1);
                        Destroy(gameObject);
                        break;
                }
            }
        }
        //print(health);
    }
}
