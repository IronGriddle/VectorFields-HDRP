using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPlacement : MonoBehaviour
{
    //The reason why this isn't packed in the same class as the HighlightDestruction.cs is because of the need to disable specific renderers.
    //Highlights where placement will be at the position of the mouse.
    // Start is called before the first frame update

    Renderer[] Renderers;

    void Start()
    {
        Renderers = gameObject.GetComponentsInChildren<Renderer>();
        setRendererEnabled(false);//Setting mesh as invisible.


    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100) && Input.GetMouseButton(0))
        {
            setRendererEnabled(true);
            //Get the center of the collided object.
            Vector3 center = WorldGrid.RoundedPoint(hit.collider.transform.position);
            //Set position.
            transform.position = center;
            //Rotate highlighter towards normal.
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        else
        {
            setRendererEnabled(false);
        }
    }

    void setRendererEnabled(bool state)
    {
        foreach (var renderer in Renderers)
        {
            renderer.enabled = state;
        }
    }


}
