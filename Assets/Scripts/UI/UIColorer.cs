using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIColorer : MonoBehaviour
{
    public UnityEvent<Color> colorChanged;

    public float h = 1;
    public float s = 1;
    public float v = 1;

    //Accessed by vfx
    public Color color;

    private void Awake()
    {
        color = new Color(1, 1, 1, 1);
    }

    //TODO: Maybe clamp these. Maybe make these actual setters. Maybe not update color in each set method.
    public void SetH(float input)
    {
        h = input;
        color = Color.HSVToRGB(h, s, v, true);
        colorChanged.Invoke(color);
    }
    public void SetS(float input)
    {
        s = input;
        color = Color.HSVToRGB(h, s, v, true);
        colorChanged.Invoke(color);
    }
    public void SetV(float input)
    {
        v = input;
        color = Color.HSVToRGB(h, s, v, true);
        colorChanged.Invoke(color);
    }


}
