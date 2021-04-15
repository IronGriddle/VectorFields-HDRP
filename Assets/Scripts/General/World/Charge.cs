using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public static string TAG = "Charge";
    public float charge = 1f;

    public Vector3 GetForceAtPoint(Vector3 point)
    {
        return charge / (transform.position - point).sqrMagnitude * Vector3.Normalize(point);
    }

    public float GetPotentialAtPoint(Vector3 point)
    {
        return charge / (transform.position - point).magnitude;
    }

}
