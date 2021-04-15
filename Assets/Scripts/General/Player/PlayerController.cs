using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    enum Mode
    {
        Building,
        Charging,
    }
    
    Mode mode = Mode.Building;

    ChargeVFXController chargeVFXController;
    BuildVFXController buildVFXController;
    PlaceCharge placeChargeBehavior;
    DestroyCharge destroyChargeBehavior;
    Charger chargerBehavior;


    public UnityEvent BuildModeActivated; 
    public UnityEvent ChargeModeActivated; 


    // Start is called before the first frame update
    void Start()
    {
        chargeVFXController = GameObject.FindObjectOfType<ChargeVFXController>();
        buildVFXController = GameObject.FindObjectOfType<BuildVFXController>();

        placeChargeBehavior = gameObject.GetComponent<PlaceCharge>();
        destroyChargeBehavior = gameObject.GetComponent<DestroyCharge>();
        chargerBehavior = gameObject.GetComponent<Charger>();

        Set2BuildMode();
        

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Set2BuildMode()
    {
        chargerBehavior.enabled = false;

        placeChargeBehavior.enabled = true;
        destroyChargeBehavior.enabled = true;
    }
    public void Set2ChargeMode()
    {
        placeChargeBehavior.enabled = false;
        destroyChargeBehavior.enabled = false;

        chargerBehavior.enabled = true;
    }

}
