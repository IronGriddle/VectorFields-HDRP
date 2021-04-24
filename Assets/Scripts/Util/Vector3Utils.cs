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

    public static Vector3 ClampMinMax(Vector3 input,  Vector3 min, Vector3 max)
    {
        float x = Mathf.Clamp(input.x, min.x, max.x);
        float y = Mathf.Clamp(input.y, min.y, max.y);
        float z = Mathf.Clamp(input.z, min.z, max.z);

        return new Vector3(x, y, z);

    }
}
