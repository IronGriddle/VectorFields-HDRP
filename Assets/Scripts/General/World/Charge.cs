using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public static string TAG = "Charge";
    public float charge = 1f;

    public Vector3 GetForceAtPoint(Vector3 point)
    {
        //First get r squared, the distance between transform.position and point
        //Then get r hat, the normal of the distance vector between transform.position and point

        Vector3 r = transform.position - point;
        return charge /r.sqrMagnitude * -Vector3.Normalize(r);
    }

    public float GetPotentialAtPoint(Vector3 point)
    {
        return charge / (transform.position - point).magnitude;
    }

}
