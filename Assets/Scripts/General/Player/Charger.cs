using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO: fix Charge prefix in +4 different classes.
//I know the name is stupid, but I currently have no idea what to name this.
public class Charger : MonoBehaviour
{

    enum MouseButtons
    {
        LMB = 0,
        RMB = 1
    }

    public UnityEvent<Vector3Int> Charging;
    public UnityEvent StoppedCharging;

    public bool positive = true;
    bool charging = false;
    GameObject objectBeingCharged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Returns charge negativity or positivity.
    float chargeDirection()
    { 
        return (positive ? 1 : -1) ;
    }

    void Update()
    {
        //discharge on right click and charge on left click
        ChargeUpdate(MouseButtons.RMB);
        ChargeUpdate(MouseButtons.LMB);
    }


    void ChargeUpdate(MouseButtons mousebutton)
    {

        if (mousebutton == MouseButtons.RMB)
        {
            positive = true;
        }
        if (mousebutton == MouseButtons.LMB)
        {
            positive = false;
        }

        if (Input.GetMouseButtonDown((int)mousebutton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                objectBeingCharged = hit.collider.gameObject;

                if (objectBeingCharged.tag == "Charge")
                {
                    objectBeingCharged = hit.collider.gameObject;
                    Charging.Invoke(WorldGrid.RoundedPoint(hit.transform.position));
                    charging = true;
                }
            }
        }

        if (charging)
        {
            StartCoroutine(Charge(objectBeingCharged));
        }

        if (Input.GetMouseButtonUp((int)mousebutton))
        {
            objectBeingCharged = null;
            StoppedCharging.Invoke();
            charging = false;
        }
    }

    private void OnDisable()
    {
        StoppedCharging.Invoke();
    }


    public float _period = 0.05f;
    bool chargeIsRunning;
    IEnumerator Charge(GameObject gameObject)
    {
        if (chargeIsRunning)
        {
            yield return null;
        }


        chargeIsRunning = true;
        if (gameObject != null)
        {
            Charge charge = gameObject.GetComponent<Charge>();

            if (positive)
            {
                charge.charge += 1f;
            }
            else if (!positive)
            {
                charge.charge -= 1f;
            }

            yield return null;
        }

        yield return new WaitForSeconds(_period);
        chargeIsRunning = false;
    }



}
