using UnityEditor;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    [SerializeField]
    BoundsInt bounds;

    public static BoundsInt RoundedBounds(Bounds bounds)
    {
        return new BoundsInt(RoundedPoint(bounds.min), RoundedPoint(bounds.max));
    }
    public static Vector3Int RoundedPoint(Vector3 point)
    {
        return new Vector3Int(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y), Mathf.RoundToInt(point.z));
    }


}
