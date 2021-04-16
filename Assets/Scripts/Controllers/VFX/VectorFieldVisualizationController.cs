using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VectorFieldVisualizationController : MonoBehaviour
{
    VisualEffect vfx; 

    // Start is called before the first frame update
    void Awake()
    {
        vfx = gameObject.GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVFXTexture3D(Texture3D texture3D)
    {
        vfx.SetTexture("_Texture3D", texture3D);
    }


}
