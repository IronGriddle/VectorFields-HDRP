using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

public class VectorField : MonoBehaviour
{
    public UnityEvent<Texture3D> UpdatedTexture3D;

    //Deals with calculations in 3D space.
    //Also contains inner functions which can be manipulated.
    public Function3D function3D;

    //Used in coloring VFX
    public float maximumMagnitude;
    public float minimumMagnitude;

    //Used to store Vector Field.
    public Texture3D texture3D;
    
    public BoundsInt boundsInt;

    public TextureWrapMode m_WrapMode = TextureWrapMode.Clamp; //Clamping texture to prevent repetition
    public FilterMode m_FilterMode = FilterMode.Trilinear;     //Trilinear for voxel interpolation.
    public TextureFormat m_textureFormat = TextureFormat.RGBAFloat;

    public bool m_GenerateMipMaps = false;
    public int m_AnisoLevel = 16;

    private void Awake()
    {

        boundsInt = new BoundsInt(-5, -5, -5, 5, 5, 5);

        //Get the Function3D component attatched or generate if null.
        function3D = gameObject.GetComponent<Function3D>();
        if (function3D == null)
        {
            //Generate and set default parameters of function3D to x y z.
            function3D = gameObject.AddComponent<Function3D>();
        }


        //Initialize Texture3D to bounds and default parameters.
        SetTexture3D();
    }

    //Calculate one octant of size, then multiply by 8 to return the amount to be calculated.
    public int GetNCalculated()
    {
        int count = 0;
        foreach (var item in boundsInt.allPositionsWithin)
        {
            count++;
        }
        return count;
    }

    //Update the Texture 3D describing this vector field.
    public void SetTexture3D()
    {
        //Creating a new cubic Texture3D of proper size. 
        texture3D = new Texture3D(boundsInt.xMax - boundsInt.xMin, boundsInt.yMax - boundsInt.yMin , boundsInt.zMax - boundsInt.zMin, m_textureFormat, m_GenerateMipMaps);
        texture3D.wrapMode = m_WrapMode;       //TextureWrapMode.Clamp
        texture3D.filterMode = m_FilterMode;   //FilterMode.Trilinear
        texture3D.anisoLevel = m_AnisoLevel;   //1 
        //Iterating over each integer point in bounds.
        foreach (Vector3Int pos in boundsInt.allPositionsWithin)
        {
            //Filling Color array with forces
            Vector3 force = function3D.CalculateAtVector3(pos);
            Color c = new Color(force.x, force.y, force.z, 1);
            texture3D.SetPixel(pos.x + Mathf.Abs(boundsInt.xMin), pos.y + Mathf.Abs(boundsInt.yMin), pos.z + Mathf.Abs(boundsInt.zMin), c);

            //Updating minimum value.
            minimumMagnitude = Mathf.Min(minimumMagnitude, force.magnitude);
            maximumMagnitude = Mathf.Max(maximumMagnitude, force.magnitude);

        }

        texture3D.Apply(true);

        UpdatedTexture3D.Invoke(texture3D);
    }



}