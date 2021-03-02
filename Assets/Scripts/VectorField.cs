using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class VectorField : MonoBehaviour
{

    //Deals with calculations in 3D space.
    //Also contains inner functions which can be manipulated.
    public Function3D function3D;

    public Texture3D texture3D;
    protected List<Vector3> forces;
    protected List<Vector3> positions;

    //Length from the center of a cube.
    //Default size means the cube calculates f(-10,-10,-10) -> f(10,10,10)
    public float sizeOfCube = 10;

    //Resolution determines how many points are calculated. The true resolution is resolution cubed and multiplied by 8
    //This is due to the nature of cubes.
    public int nonCubedResolution = 10;

    
    private TextureWrapMode m_WrapMode = TextureWrapMode.Clamp; //Clamping textureto prevent repetition
    private FilterMode m_FilterMode = FilterMode.Trilinear;     //Trilinear for voxel interpolation.
    public bool m_GenerateMipMaps = false;
    public int m_AnisoLevel = 1;

    private void Awake()
    {
        //get the Function3D component attatched or generate if null.
        function3D = gameObject.GetComponent<Function3D>();
        if (function3D == null)
        {
            //Generate and set parameters of function3D to x y z.
            function3D = gameObject.AddComponent<Function3D>();
            function3D.parameters = new List<string>() { "x", "y", "z" };
        }
    }
    private void Start()
    {
        //Initialize Position, forces, and texture3D to defaults.
        UpdateVectorField();
    }

    //Update the entire system.
    public virtual void UpdateVectorField()
    {
        SetPositions();
        SetForces();
        SetTexture3D();
    }

    //Update forces at each position.
    protected virtual void SetForces()
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

        //FOREACH VERSION. DOES NOT MAINTAIN INDEX IN LIST.
        //Parallel.ForEach(positions, pos => forces.Add(function3D.CalculateAtPosition(pos)));
    }

    //Possible bug here with step.
    protected virtual void SetPositions()
    {
        //First calculate the step size.
        float step = sizeOfCube / nonCubedResolution;

        //Then create a list of positions to calculate force at
        positions = new List<Vector3>();
        
        //Then generate and add points to positions
        for (float z = -sizeOfCube; z < sizeOfCube; z+= step)
        {
            for (float y = -sizeOfCube; y < sizeOfCube; y+= step)
            {
                for (float x = -sizeOfCube; x < sizeOfCube; x++)
                {
                    positions.Add(new Vector3(x, y, z));
                }
            }
        }
    }

    //Calculate one octant of size, then multiply by 8 to return the amount to be calculated.
    public int GetNCalculated()
    {
        return nonCubedResolution * nonCubedResolution * nonCubedResolution * 8;
    }

    //Update the Texture 3D describing this vector field.
    protected virtual void SetTexture3D()
    {   
        //Creating a new cubic Texture3D of proper size. 
        texture3D = new Texture3D(nonCubedResolution*2, nonCubedResolution * 2, nonCubedResolution * 2, TextureFormat.RGBAHalf, m_GenerateMipMaps);
        texture3D.wrapMode = m_WrapMode;       //TextureWrapMode.Clamp
        texture3D.filterMode = m_FilterMode;   //FilterMode.Trilinear
        texture3D.anisoLevel = m_AnisoLevel;   //1 
        //Creating color array of size forces.count
        Color[] colors = new Color[forces.Count];

        //Filling Color array with forces.
        for (int i = 0; i < forces.Count; i++)
        {
            Color c;
            Vector3 force = forces[i];
            c = new Color(force.x, force.y, force.z);
            colors[i] = c;
        }

        //Setting voxels of texture.
        texture3D.SetPixels(colors);
        texture3D.Apply(true, true);
    }

}
