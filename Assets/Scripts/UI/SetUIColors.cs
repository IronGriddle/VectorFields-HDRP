using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUIColors : MonoBehaviour
{

    public Color color;
    public UIColorer particleColor;


    [SerializeField] List<Slider> sliders;
    


    // Start is called before the first frame update
    void Start()
    {
        particleColor = gameObject.GetComponent<UIColorer>();
    }

    // Update is called once per frame
    void Update()
    {
        color = particleColor.color;
    }

    public void UpdateColor()
    {
        foreach (Slider slider in sliders)
        {
            slider.image.color = color;
        }
    }


}
