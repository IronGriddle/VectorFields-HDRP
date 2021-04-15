using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeVFXController : MonoBehaviour
{
    FadeShaderAlpha CubeVFXAlphaController;

    // Start is called before the first frame update
    void Start()
    {
        CubeVFXAlphaController = gameObject.GetComponentInChildren<FadeShaderAlpha>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableChargeVFX()
    {
        gameObject.SetActive(false);
    }

    public void EnableChargeVFX()
    {
        gameObject.SetActive(true);

    }

}
