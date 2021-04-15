using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPlayerFromCharges : MonoBehaviour
{
    Vector3 targetPosition;
    public GameObject[] Charges;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    //ACCESSED FROM UNITY EDITOR EVENT SYSTEM
    public void OnChange()
    {
        
        Charges = GameObject.FindGameObjectsWithTag("Charge");
        Vector3 totalPoints = Vector3.zero;
        foreach (var charge in Charges)
        {
            totalPoints += charge.transform.position;
        }

        targetPosition = totalPoints / Charges.Length;
          
    }



}
