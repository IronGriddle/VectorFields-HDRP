using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Utils : MonoBehaviour
{
    public static Vector3Int RoundedPoint(Vector3 point)
    {
        return new Vector3Int(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y), Mathf.RoundToInt(point.z));
    }

    public static Color V3Color(Vector3 color, float alpha = 1)
    {
        return new Color(color.x, color.y, color.z, alpha);
    }

}
