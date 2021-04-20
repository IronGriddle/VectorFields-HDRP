using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator : MonoBehaviour
{
    public static List<Vector3> Vortex (List<Vector3> positions){
        List<Vector3> forces = new List<Vector3>();
        foreach (Vector3 position in positions)
        {
            forces.Add(VortexExpression(position.x, position.y, position.z));
        }
        return forces;


    }

    static Vector3 VortexExpression(float x, float y, float z)
    {
        return new Vector3(
            (-y) / (Mathf.Pow(x,2) + Mathf.Pow(y,2)),
            x / (Mathf.Pow(x, 2) + Mathf.Pow(y, 2)),
            0
            );
    }
}
