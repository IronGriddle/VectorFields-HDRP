using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceCharge : MonoBehaviour
{
    [SerializeField]
    GameObject ObjectToBePlaced;
    
    public UnityEvent OnPlace;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !placeIsRunning){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                StartCoroutine(Place(hit));
            }
        }
    }

    public float _placeCoolDown = 1f;
    bool placeIsRunning = false;
    IEnumerator Place(RaycastHit hit)
    {

        placeIsRunning = true;

        Instantiate(ObjectToBePlaced, WorldGrid.RoundedPoint(hit.point), Quaternion.identity);
        OnPlace.Invoke();

        yield return new WaitForSeconds(_placeCoolDown);
        placeIsRunning = false;
    }


}
