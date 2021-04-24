using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinMaxDisplay : MonoBehaviour
{
    public Slider slider;
    public GameObject text;
    TextMeshPro textMeshPro;

    // Start is called before the first frame update
    private void Start()
    {
        textMeshPro = text.GetComponent<TextMeshPro>();
        slider = gameObject.GetComponent<Slider>();
    }


    // Update is called once per frame
    void Update()
    {
        //textMeshPro.text = slider.value.ToString();
    }
}
