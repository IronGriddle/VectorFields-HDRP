using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VectorFieldVFXController : MonoBehaviour
{
    VisualEffect vfx;
    VectorField vectorField;

    // Start is called before the first frame update
    void Awake()
    {
        vfx = gameObject.GetComponent<VisualEffect>();
        vectorField = FindObjectOfType<VectorField>();
    }

    private void Start()
    {
        StartCoroutine(DelayedUpdate());
    }



    IEnumerator DelayedUpdate()
    {
        while (true)
        {
            SetBounds();
            SetTexture3D();
            yield return new WaitForSeconds(1f);
        }
    }


    void SetTexture3D()
    {
        vfx.SetTexture("Texture3D", vectorField.texture3D);
    }

    void SetBounds()
    {
        vfx.SetVector3("Center", vectorField.bounds.center);
        vfx.SetVector3("Size", vectorField.bounds.size);
    }



}
