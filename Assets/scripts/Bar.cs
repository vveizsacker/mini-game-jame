using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public Transform barGfx;
    float maxValue;
    float value;

    public void SetMaxValue(float v)
    {
        maxValue = v;
        value = v;
    }

    public void updateValue(float v)
    {
        value = v;

        float p = value / maxValue;
        float r = 1 - p;

        barGfx.localPosition = new Vector2(-(r / 2), 0); ;
        barGfx.localScale = new Vector2(p,1);
    }

}
