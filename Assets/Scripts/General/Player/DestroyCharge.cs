using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyCharge : MonoBehaviour
{


    public UnityEvent<Vector3Int> _Destroy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Right button destroys
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

        Vector3Int position = WorldGrid.RoundedPoint(hit.collider.transform.position);
        Destroy(hit.collider.gameObject); //Destroying gameobject.
        _Destroy.Invoke(position); //Activating _Destroy event.


        yield return new WaitForSeconds(_destroyCoolDown);
        destroyIsRunning = false;
    }

}
