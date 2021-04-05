using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    [SerializeField]
    BoundsInt bounds;
    Texture3D texture3D;


    // Start is called before the first frame update
    void Start()
    {
        texture3D = new Texture3D(bounds.xMax, bounds.zMax, bounds.yMax, TextureFormat.Alpha8, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetPointInTexture3D(Vector3 point)
    {
        Vector3Int index = Point2Index(point);
        if (bounds.Contains(index))
        {
            texture3D.SetPixel(index.x, index.y, index.z, Color.white);
        }
    }

    public static Vector3Int Point2Index(Vector3 point)
    {
        return new Vector3Int(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y), Mathf.RoundToInt(point.z));
    }

    //Save Texture 3D for debugging
    private void OnApplicationQuit()
    {
        string name = System.DateTime.Now.ToLongTimeString();
        AssetDatabase.CreateAsset(texture3D, "Assets/Scripts/General/World/Saves/" + name + ".asset");
    }


}
