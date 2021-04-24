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

    public void SetParticleIntensity(float force)
    {
        vfx.SetFloat("Intensity", force);
    }

    public void SetColor(Color color)
    {
        vfx.SetVector3("Color", new Vector3(color.r, color.g, color.b));
    }

    public void SetParticleSize(float force)
    {
        vfx.SetFloat("Particle_Size", force);
    }

    IEnumerator DelayedUpdate()
    {
        while (true)
        {
            SetDataSize();
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

    void SetDataSize()
    {
        vfx.SetInt("Data_Size", vectorField.Data_Size);
    }

}
