using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorListener : MonoBehaviour
{
    public GameObject objectWithFunction3D;
    Function3D function3D;
    TextMeshProUGUI ErrorText;
    private void Start()
    {
        function3D = objectWithFunction3D.GetComponent<Function3D>();
        ErrorText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ErrorText.SetText(function3D.Error);
    }
}
