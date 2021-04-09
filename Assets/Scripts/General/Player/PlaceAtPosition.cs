using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceAtPosition : MonoBehaviour
{

    [SerializeField]
    KeyCode key;
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
        if (Input.GetKey(key) && !placeIsRunning){
            StartCoroutine(Place());
        }
    }

    float placeCoolDown = 1f;
    bool placeIsRunning = false;
    IEnumerator Place()
    {
        placeIsRunning = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);

            Instantiate(ObjectToBePlaced, WorldGrid.Point2Index(hit.point), Quaternion.identity);
            OnPlace.Invoke();
        }

        yield return new WaitForSeconds(placeCoolDown);
        placeIsRunning = false;
    }


}
