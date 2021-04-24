using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureUtils : MonoBehaviour
{

    //1D array Rfloat
    public static Texture2D FloatList2Texture2D(List<float> floats)
    {
        Texture2D texture2D = new Texture2D(floats.Count, 1, TextureFormat.RFloat, false);


        Color[] colors = new Color[floats.Count];
        for (int i = 0; i < floats.Count; i++)
        {
            colors[i] = new Color(floats[i],0,0);
        }

        texture2D.filterMode = FilterMode.Point;
        texture2D.wrapMode = TextureWrapMode.Clamp;
        texture2D.SetPixels(colors);

        texture2D.Apply();

        return texture2D;
    }
    //2D array
    public static Texture2D FloatList2Texture2D(List<float> floats, bool is2D = true)
    {
        if (!is2D){
            return FloatList2Texture2D(floats);
        }


        int size = Mathf.CeilToInt(Mathf.Sqrt(floats.Count));

        Texture2D texture2D = new Texture2D(size, size, TextureFormat.RFloat, false);


        Color[] colors = new Color[size * size];
        for (int i = 0; i < floats.Count; i++)
        {
            colors[i] = new Color(floats[i], 0, 0);
        }

        texture2D.filterMode = FilterMode.Point;
        texture2D.wrapMode = TextureWrapMode.Clamp;
        texture2D.SetPixels(colors);

        texture2D.Apply();

        return texture2D;
    }



    //1D array
    public static Texture2D Vector3List2Texture2D(List<Vector3> vectors)
    {




        Texture2D texture2D = new Texture2D(vectors.Count, 1, TextureFormat.RGBAFloat, false);


        Color[] colors = new Color[vectors.Count];
        for (int i = 0; i < vectors.Count; i++)
        {
            colors[i] = Vector3Utils.V3Color(vectors[i]);
        }

        texture2D.filterMode = FilterMode.Point;
        texture2D.wrapMode = TextureWrapMode.Clamp;
        texture2D.SetPixels(colors);

        texture2D.Apply();

        return texture2D;
    }

    //2D array
    public static Texture2D Vector3List2Texture2D(List<Vector3> vectors, bool is2D = true)
    {
        if (!is2D)
        {
            return Vector3List2Texture2D(vectors);
        }

        int size = Mathf.CeilToInt(Mathf.Sqrt(vectors.Count));

        Texture2D texture2D = new Texture2D(size, size, TextureFormat.RGBAFloat, false);

        Color[] colors = new Color[size * size];
        for (int i = 0; i < vectors.Count; i++)
        {
            colors[i] = Vector3Utils.V3Color(vectors[i]);
        }


        texture2D.filterMode = FilterMode.Point;
        texture2D.wrapMode = TextureWrapMode.Clamp;
        texture2D.SetPixels(colors);

        texture2D.Apply();

        return texture2D;
    }



    //BEWARE, SIZE OF TEXTURE3D GENERATED WITH THIS METHOD CAN BE ABSOLUTELY MASSIVE.
    //First generates a Texture3D by 
    public static Texture3D PositionsAndForces2Texture3D(List<Vector3> positions, List<Vector3> forces, TextureFormat m_textureFormat = TextureFormat.RGBAFloat, bool m_GenerateMipMaps = false, TextureWrapMode m_WrapMode = TextureWrapMode.Clamp, FilterMode m_FilterMode = FilterMode.Point, int m_AnisoLevel = 16)
    {

    //Getting max vector from list.
    float maxX = positions[0].x;
        float maxY = positions[0].y;
        float maxZ = positions[0].z;
        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i].x > maxX) maxX = positions[i].x;
            if (positions[i].y > maxY) maxY = positions[i].y;
            if (positions[i].z > maxZ) maxZ = positions[i].z;
        }
        Vector3 maximum = new Vector3(maxX, maxY, maxZ);//Setting maximum vector.


        //Getting min vector from list.
        float minX = positions[0].x;
        float minY = positions[0].y;
        float minZ = positions[0].z;

        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i].x < minX) minX = positions[i].x;
            if (positions[i].y < minY) minY = positions[i].y;
            if (positions[i].z < minZ) minZ = positions[i].z;
        }
        Vector3 minimum = new Vector3(minX, minY, minZ);//Setting minimum vector

        Vector3Int offsetMax = Vector3Utils.RoundedPoint(maximum - minimum);


        //Creating texture with proper size
        Texture3D texture3d = new Texture3D(offsetMax.x, offsetMax.y, offsetMax.z, m_textureFormat, m_GenerateMipMaps);
        texture3d.wrapMode = m_WrapMode;       //TextureWrapMode.Clamp
        texture3d.filterMode = m_FilterMode;   //FilterMode.Trilinear
        texture3d.anisoLevel = m_AnisoLevel;   //1 ;

        //Offsetting all positions such that they are all greater than the zero vector <0,0,0>
        //This is used when storing into Texture3D to avoid negative indexes.
        for (int i = 0; i < positions.Count; i++)
        {
            positions[i] = positions[i] - minimum;
        }

        //Finally setting forces in Texture3D from offset positions.
        for (int i = 0; i < positions.Count; i++)
        {

            Vector3Int posInt = Vector3Utils.RoundedPoint(positions[i]);

            texture3d.SetPixel(posInt.x, posInt.y, posInt.z, Vector3Utils.V3Color(forces[i]));
        }

        texture3d.Apply();

        return texture3d;
    }
}
