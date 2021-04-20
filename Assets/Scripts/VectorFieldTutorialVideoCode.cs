using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorFieldTutorialVideoCode : MonoBehaviour
{
    List<Vector3> positions;
    List<Vector3> forces;


    void SetForces() {
        foreach (Vector3 position in positions)
        {
            Vector3 force;
            force.x = xForce(position);
            force.y = yForce(position);
            force.z = zForce(position);

            forces.Add(force);
        } 
    }



    float xForce (Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        return x + y + z * y;
    }

    float yForce(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        return x + y + z * y;
    }

    float zForce(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        return x + y + z * y;
    }
}
