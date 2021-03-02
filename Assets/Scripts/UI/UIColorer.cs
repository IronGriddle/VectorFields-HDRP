using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColorer : MonoBehaviour
{
    public float h = 0;
    public float s = 0;
    public float v = 0;

    public Color color;

    //TODO: Maybe clamp these. Maybe make these actual setters. Maybe not update color in each set method.
    public void SetH(float input)
    {
        h = input;
        color = Color.HSVToRGB(h, s, v, true);
    }
    public void SetS(float input)
    {
        s = input;
        color = Color.HSVToRGB(h, s, v, true);
    }
    public void SetV(float input)
    {
        v = input;
        color = Color.HSVToRGB(h, s, v, true);
    }


}
