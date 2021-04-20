using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VectorFieldVisualizationController : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        


    }



    IEnumerator DelayedUpdate()
    {
        while (true)
        {
            SetForces();
            SetPositions();
            yield return new WaitForSeconds(1f);
        }
    }


    void SetForces()
    {
        vfx.SetTexture("Forces", vectorField.forceMap);
    }

    void SetPositions()
    {
        vfx.SetTexture("Positions", vectorField.positionMap);
    }

}
