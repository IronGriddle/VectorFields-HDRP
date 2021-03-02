using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXParticleSettings : MonoBehaviour
{
    public GameObject colorHandler;
    public UIColorer colorer;


    public Color color;
    public string PColor = "particle_color";
    public string PIntensity = "particle_intensity";
    public string PSize = "particle_size";
    public string PScaleY = "particle_scale_y";
    public string PScaleX = "particle_scale_x";
    public VisualEffect vfx;

    void Start()
    {
        colorer = colorHandler.GetComponent<UIColorer>();
        vfx = GetComponent<VisualEffect>();
    }

    void Update()
    {
        
    }

    public void SetParticleIntensity(float amount)
    {
        vfx.SetFloat(PIntensity, amount);
    }

    public void SetParticleColor()
    {
        vfx.SetVector4(PColor, colorer.color);
    }

    public void SetParticleSize(float amount)
    {
        vfx.SetFloat(PSize, amount);
    }

    public void SetParticleY(float amount)
    {
        vfx.SetFloat(PScaleY, amount);
    }

    public void SetParticleX(float amount)
    {
        vfx.SetFloat(PScaleX, amount);
    }


}
