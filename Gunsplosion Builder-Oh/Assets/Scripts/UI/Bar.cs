using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public RectTransform meter;
    private const float maxWidth = 462;

    public void SetMeter(float val) {
        meter.sizeDelta = new Vector2(maxWidth * val, meter.sizeDelta.y);
    }
}
