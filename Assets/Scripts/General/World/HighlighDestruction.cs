using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlighDestruction : MonoBehaviour
{

    Renderer _Renderer;
    

    // Start is called before the first frame update
    void Start()
    {
        _Renderer = gameObject.GetComponent<Renderer>();
        _Renderer.enabled = false;
    
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100) && Input.GetMouseButton(1))
        {
            _Renderer.enabled = true;
            //Get the center of the collided object.
            Vector3 center = WorldGrid.RoundedPoint(hit.collider.transform.position);
            //Set position.
            transform.position = center;
        }
        else
        {
            _Renderer.enabled = false;
        }
        
    }
}
