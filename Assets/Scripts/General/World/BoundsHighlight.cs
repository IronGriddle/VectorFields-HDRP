using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoundsHighlight : MonoBehaviour
{

    Renderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        Renderer = gameObject.GetComponent<Renderer>();
        StartCoroutine(UpdateHighlight());

    }


    
    //Updates bounds highlight to look correct based on scale.
    //Might want to refactor and update shader to deal with non-cubic bounds.
    void Update()
    {
    }

    public float secondsWaited = 0.05f;
    public bool updateHighlight = true;
    IEnumerator UpdateHighlight()
    {

        while (updateHighlight)
        {
            Renderer.material.SetFloat("_Length", transform.localScale.x);
            yield return new WaitForSeconds(secondsWaited);
        }


    }

}
