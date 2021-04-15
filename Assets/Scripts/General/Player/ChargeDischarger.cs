using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChargeDischarger : MonoBehaviour
{
    public UnityEvent<Vector3Int> OnDischarge;

    void Update()
    {
        if (Input.GetMouseButton(0) && !chargeIsRunning)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                StartCoroutine(Charge(hit));
                OnDischarge.Invoke(WorldGrid.RoundedPoint(hit.transform.position));
            }
        }
    }

    public float _rate = 0.05f;
    bool chargeIsRunning;
    IEnumerator Charge(RaycastHit hit)
    {

        chargeIsRunning = true;
        Charge charge = hit.collider.gameObject.GetComponent<Charge>();
        charge.charge -= 0.1f;
        yield return new WaitForSeconds(_rate);
        chargeIsRunning = false;
    }



}
