using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B83.ExpressionParser;
using UnityEditor;
using System.Threading.Tasks;
using System.Diagnostics;

public class VectorField : MonoBehaviour
{
    //This thing looks nasty.
    //I must fix it.

    //Deals with calculations in 3D space.
    //Also is set to calculate certain functions.
    public Function3D function3D;

    public Texture3D texture;
    protected List<Vector3> forces;
    protected List<Vector3> positions;

    //Size determines where points are calculated at. Such as f(1,0), f(2,0) -> f(10,0)
    public float size = 10;

    //Resolution determines how many points are calculated. The true resolution is resolution cubed and multiplied by 8
    //This is due to the nature of cubes.
    public int resolution = 100;

    //Clamping to prevent repetition
    public TextureWrapMode m_WrapMode = TextureWrapMode.Clamp;

    //Trilinear for voxels.
    public FilterMode m_FilterMode = FilterMode.Trilinear;
    public bool m_GenerateMipMaps = false;
    public int m_AnisoLevel = 1;

    private void Awake()
    {
        //get the component attatched.
        function3D = gameObject.GetComponent<Function3D>();
    }
    private void Start()
    {
        UpdateVectorField();
    }

    public int GetNCalculated()
    {
        //Calculate one octant of size, then multiply by 8 to return the amount to be calculated.
        return resolution * resolution * resolution * 8;
    }

    public virtual void UpdateVectorField()
    {
        UpdatePositions();
        UpdateForces();
        UpdateTexture3D();
    }

    //Update forces at each position.
    protected virtual void UpdateForces()
    {
        forces = new List<Vector3>(new Vector3[positions.Count]);

        //Calculate force at each voxel. 
        //Using for loop to maintain position in list.
        Parallel.For(0, positions.Count,
            i =>
            forces[i] = function3D.CalculateAtPosition(positions[i]));

        //Sequential Version

        //forces = new List<Vector3>();
        //foreach (var position in positions)
        //{
        //    //While they may have all parameters filled out (xyz), unused parameters will simply be ignored.
        //    forces.Add(function3D.CalculateAtPosition(position));
        //}

        //DO NOT USE FOREACH VERSION. DOES NOT MAINTAIN INDEX IN LIST.
        //Parallel.ForEach(positions, pos => forces.Add(function3D.CalculateAtPosition(pos)));
    }

    protected virtual void UpdatePositions()
    {
        //First initialize a list of positions.
        //This list will be of size GetNCalculated();


        //First calculate the step size.
        float step = size / resolution;

        //Then create a list of positions to calculate force at
        positions = new List<Vector3>();
        
        //Iterate over a cube of length size * 2
        for (float z = -size; z < size; z+= step)
        {
            for (float y = -size; y < size; y+= step)
            {
                for (float x = -size; x < size; x++)
                {
                    positions.Add(new Vector3(x, y, z));
                }
            }
        }
    }

    //Update the Texture 3D describing this vector field.
    protected virtual void UpdateTexture3D()
    {   
        //Creating a new cubic Texture3D of proper size. 
        texture = new Texture3D(resolution*2, resolution * 2, resolution * 2, TextureFormat.RGBAHalf, m_GenerateMipMaps);
        texture.wrapMode = m_WrapMode;       //TextureWrapMode.Clamp
        texture.filterMode = m_FilterMode;   //FilterMode.Trilinear
        texture.anisoLevel = m_AnisoLevel;   //1 
        //Creating color array of size forces.count
        Color[] colors = new Color[forces.Count];

        for (int i = 0; i < forces.Count; i++)
        {
            Color c;
            Vector3 force = forces[i];
            c = new Color(force.x, force.y, force.z);
            colors[i] = c;
        }

        texture.SetPixels(colors);
        texture.Apply(true, true);
    }

}
