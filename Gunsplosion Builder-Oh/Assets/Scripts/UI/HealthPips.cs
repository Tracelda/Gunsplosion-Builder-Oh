using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPips : MonoBehaviour
{
    public GameObject pipPrefab;
    public int maxHealth;
    public Sprite emptySprite, filledSprite;
    private List<Image> pips = new List<Image>();

    private void Start()
    {
        for(int i = 0; i < maxHealth; i++)
        {
            pips.Add(Instantiate(pipPrefab, transform).GetComponent<Image>());
        }
    }

    public void UpdateHealth(int val)
    {
        val = Mathf.Clamp(val, 0, maxHealth);
        for(int i = 0; i < pips.Count; i++)
        {
            if (i < val)
            {
                pips[i].sprite = filledSprite;
            }
            else
            {
                pips[i].sprite = emptySprite;
            }
        }
    }

    public void UpdateHealthPercentage(float val)
    {
        UpdateHealth(Mathf.FloorToInt(val * maxHealth));
    }
}
