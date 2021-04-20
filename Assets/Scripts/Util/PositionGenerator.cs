using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerator : MonoBehaviour
{

    //Returns integer positions within givin bounds.
    public static List<Vector3> BoundsIntegerPositions(Bounds bounds)
    {
       
        List<Vector3> positions = new List<Vector3>();

        for (float z = bounds.min.z; z < bounds.max.z; z++)
        {
            for (float y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (float x = bounds.min.x; x < bounds.max.x; x++)
                { 
                    positions.Add(new Vector3(x, y, z));
                }
            }
        }
        return positions;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
