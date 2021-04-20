using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEditor;

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
    public Texture2D positionMap;
    public Texture2D forceMap;

    //Index of force : position should be the same.
    //Used to store forces.
    public List<Vector3> forces;
    //Used to store positions.
    public List<Vector3> positions;

    public int Data_Size;

    public Bounds bounds;

    private void Awake()
    {

        bounds = new Bounds();
        bounds.min = new Vector3(-5, -5, -5);
        bounds.max = new Vector3(5, 5, 5);

        print(bounds.size);

        //Get the Function3D component attatched or generate if null.
        function3D = gameObject.GetComponent<Function3D>();
        if (function3D == null)
        {
            //Generate and set default parameters of function3D to x y z.
            function3D = gameObject.AddComponent<Function3D>();
        }


        //Initialize Texture3D to bounds and default parameters.
        UpdateTexture3D();

    }

    private void Start()
    {
    }

    void UpdatePositions()
    {
        //List of positions to calculate force at
        positions = new List<Vector3>();

        //Generating and adding points to positions
        positions = PositionGenerator.BoundsIntegerPositions(bounds);

        positionMap = TextureUtils.Vector3List2Texture2D(positions);

        Data_Size = positions.Count;
    }
    void UpdateForces()
    {
        forces = new List<Vector3>(new Vector3[positions.Count]);

        //Calculate force at each voxel. 
        //Using for loop to maintain position in list.

        for (int i = 0; i < positions.Count; i++)
        {
            forces[i] = function3D.CalculateAtVector3(positions[i]);
        }

        //Parallel.For(0, positions.Count,
        //    i =>
        //    forces[i] = function3D.CalculateAtVector3(positions[i]));

        forceMap = TextureUtils.Vector3List2Texture2D(forces);
    }

    //Update the Texture 3D describing this vector field.
    public void UpdateTexture3D()
    {
        UpdatePositions();
        UpdateForces();
        //Creating a new cubic Texture3D of proper size. 
        texture3D = TextureUtils.PositionsAndForces2Texture3D(positions, forces);

        UpdatedTexture3D.Invoke(texture3D);
    }
    
    public void SaveAll()
    {
        int id = Random.Range(0, 10000000);

        AssetDatabase.CreateAsset(texture3D, Application.dataPath + "/Generated/fieldTexture" + id + ".asset");
        AssetDatabase.CreateAsset(positionMap, Application.dataPath + "/Generated/positionMap" + id + ".asset");
        AssetDatabase.CreateAsset(forceMap, Application.dataPath +  "/Generated/forceMap" + id + ".asset");

    }


}