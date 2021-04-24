using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Remember to include this
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;

public class VFXFieldController : MonoBehaviour
{
    public GameObject VFXobject;


    //The class VectorField creates its Texture3D with its expressions and positions.
    //The class VisualEffect describes the actual particles moving around.
    public VectorField vectorField;
    private VisualEffect vfx;

    public float playRate = 1f;

    public string VBounds = "vector_field_bounds";
    public string VScale = "vector_field_scale";
    public string VTexture3D = "vector_field";

    public float intensity;

    void Start()
    {
        vectorField = GetComponent<VectorField>();
        vfx = VFXobject.GetComponent<VisualEffect>();
    }

    void Update()
    {
        vfx.playRate = playRate;
        vfx.SetFloat("vector_field_intensity", intensity);
    }

    public void SetScale(float scale)
    {
        vfx.SetFloat(VScale, scale);
    }

    public void UpdateVectorField()
    {
        vectorField.UpdateTexture3D();
        vfx.SetTexture(VTexture3D, vectorField.texture3D);
    }

    public void SetIntensity(float newIntensity)
    {
        intensity = newIntensity;
    }
    public void SetPlayRate(float newRate)
    {
        playRate = newRate;
    }
    public void SetYExpression(string expr)
    {
        vectorField.function3D.SetExprY(expr);
    }
    public void SetZExpression(string expr)
    {
        vectorField.function3D.SetExprZ(expr);
    }
    public void SetXExpression(string expr)
    {
        vectorField.function3D.SetExprX(expr);
    }

}
