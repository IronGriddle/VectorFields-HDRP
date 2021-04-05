using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCharge : MonoBehaviour
{
    [SerializeField]
    float charge = 1;//Charge of the particle.

    const float BLUE_TEMP = 20000;
    const float RED_TEMP = 1500;

    Light Light;//Light at the location of the PointCharge
    

    // Start is called before the first frame update
    void Start()
    {
        Light = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Light.intensity = Mathf.Abs(charge) * 10;

        if (charge < 0)
        {
            Light.colorTemperature = RED_TEMP;
        }
        else
        {
            Light.colorTemperature = BLUE_TEMP;
        }

    }

    public void SetCharge(float newCharge)
    {
        charge = newCharge;
    }

}
