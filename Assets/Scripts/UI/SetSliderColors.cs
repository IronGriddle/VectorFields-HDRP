using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Sets the image color of all children sliders.
public class SetSliderColors : MonoBehaviour
{

    Color color;
    UIColorer uiColorer;
    Slider[] sliders;
    


    // Start is called before the first frame update
    void Start()
    {
        uiColorer = gameObject.GetComponent<UIColorer>();
        if (uiColorer == null)
        {
            uiColorer = gameObject.AddComponent<UIColorer>();
        }
        sliders = gameObject.GetComponentsInChildren<Slider>();
        color = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateColor();
        color = uiColorer.color;
    }

    public void UpdateColor()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].image.color = color;
        }
    }


}
