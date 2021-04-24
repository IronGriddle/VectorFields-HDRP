using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElectricFieldStaticController : MonoBehaviour
{
    VisualEffect vfx;
    ElectricField electricField;

    // Start is called before the first frame update
    void Awake()
    {
        vfx = gameObject.GetComponent<VisualEffect>();
        electricField = FindObjectOfType<ElectricField>();
    }

    private void Start()
    {
        StartCoroutine(DelayedUpdate());
    }

    // Update is called once per frame
    void Update()
    {



    }


    IEnumerator DelayedUpdate()
    {
        while (true)
        {
            vfx.Reinit();
            SetVoltages();
            SetDataSize();
            SetForces();
            SetPositions();
            yield return new WaitForSeconds(0.5f);
        }
    }


    void SetForces()
    {
        vfx.SetTexture("Forces", electricField.forceMap);
    }

    void SetPositions()
    {
        vfx.SetTexture("Positions", electricField.positionsCalculatedAtMap);
    }
    void SetVoltages()
    {
        vfx.SetTexture("Voltages", electricField.voltageMap);
    }

    void SetDataSize()
    {
        vfx.SetInt("Data_Size", electricField.GetDataSize());
    }


}
