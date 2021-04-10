using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCharge : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //RMB destroys
        if (Input.GetMouseButtonUp(1) && !destroyIsRunning)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                StartCoroutine(DestroyChargeAtHit(hit));
            }
        }
    }

    public float _destroyCoolDown = 1f;
    bool destroyIsRunning = false; //used as a flag.
    IEnumerator DestroyChargeAtHit(RaycastHit hit)
    {

        destroyIsRunning = true;

        Destroy(hit.collider.gameObject);

        yield return new WaitForSeconds(_destroyCoolDown);
        destroyIsRunning = false;
    }

}
