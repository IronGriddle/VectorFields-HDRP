using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElectricFieldDynamicController : MonoBehaviour
{
    VisualEffect vfx;
    ElectricField electricField;
    BoundsSize boundsSize;
    // Start is called before the first frame update
    void Awake()
    {
        boundsSize = FindObjectOfType<BoundsSize>();
        vfx = gameObject.GetComponent<VisualEffect>();
        electricField = FindObjectOfType<ElectricField>();
    }

    private void Start()
    {
        StartCoroutine(DelayedUpdate());
    }


    IEnumerator DelayedUpdate()
    {
        while (true)
        {
            UpdateTexture3D();
            UpdateCenter();
            UpdateSize();
            yield return new WaitForSeconds(0.5f);
        }
    }
    void UpdateTexture3D()
    {
        vfx.SetTexture("Texture3D", electricField.fieldTexture);
    }

    void UpdateCenter()
    {
        vfx.SetVector3("Center", boundsSize.paddedBounds.center);
    }
    void UpdateSize()
    {
        vfx.SetVector3("Size", boundsSize.paddedBounds.size);
    }



}
