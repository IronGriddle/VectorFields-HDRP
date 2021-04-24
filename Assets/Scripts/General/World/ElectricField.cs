using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectricField : MonoBehaviour
{
    Charge[] Charges;

    //RGB corresponds to the field
    //A corresponds to potential at that point

    //Index of force : position should be the same.
    //Used to store forces and voltages.
    public List<Vector3> forces;
    //Used to store positions.
    public List<Vector3> positionsCalculatedAt;
    //Used to store voltages.
    public List<float> voltages;

    //Used to store Vector Field.
    public Texture3D fieldTexture;
    public Texture2D positionsCalculatedAtMap;
    public Texture2D forceMap;
    public Texture2D voltageMap;

    public int Data_Count; //number of pixels stored inside of fieldTexture. Created from bounds.

    BoundsSize boundsSize;
    Bounds bounds;

    private void Awake()
    {
        UpdateChargeList();
        boundsSize = FindObjectOfType<BoundsSize>();
        UpdatePositions();
        UpdateForces();
        UpdateVoltages();
        UpdateTexture3D();
        Data_Count = GetDataSize();
        StartCoroutine(DelayedUpdate());
    }


    public float updatePeriod = 0.5f;
    public bool updateRunning = true;
    IEnumerator DelayedUpdate()
    {
        while (updateRunning)
        {
            print("updating");
            UpdateChargeList();
            UpdatePositions();
            UpdateForces();
            UpdateVoltages();
            UpdateTexture3D();
            Data_Count = GetDataSize();
            yield return new WaitForSeconds(updatePeriod);
        }
    }

    public void UpdateChargeList()
    {
        //dummy variable used in null checking.
        //I believe this might be used via editor. Might need to refactor to remove.
        Charge[] charges = FindObjectsOfType<Charge>();
        if (charges.Length != 0)
        {
            Charges = charges;
        }
    }

    public int GetDataSize()
    {
        return positionsCalculatedAt.Count;
    }

    void UpdatePositions()
    {
        //List of positions to calculate force at
        positionsCalculatedAt = new List<Vector3>();

        //Updating bounds;
        bounds = boundsSize.paddedBounds;

        //Generating and adding points to positions
        positionsCalculatedAt = PositionGenerator.BoundsIntegerPositions(bounds);

        positionsCalculatedAtMap = TextureUtils.Vector3List2Texture2D(positionsCalculatedAt, true);
    }

    void UpdateForces()
    {
        forces = new List<Vector3>();

        //Calculate force at each voxel. 
        //Using for loop to maintain position in list.

        for (int i = 0; i < positionsCalculatedAt.Count; i++)
        {
            forces.Add(NetForceAtPoint(positionsCalculatedAt[i]));
        }

        //Parallel.For(0, positions.Count,
        //    i =>
        //    forces[i] = function3D.CalculateAtVector3(positions[i]));

        forceMap = TextureUtils.Vector3List2Texture2D(forces, true);
    }

    void UpdateVoltages()
    {
        voltages = new List<float>();

        for (int i = 0; i < positionsCalculatedAt.Count; i++)
        {
            voltages.Add(NetPotentialAtPoint(positionsCalculatedAt[i]));
        }
        voltageMap = TextureUtils.FloatList2Texture2D(voltages, true);
    }

    public void UpdateTexture3D()
    {
        fieldTexture = TextureUtils.PositionsAndForces2Texture3D(positionsCalculatedAt, forces);
    }

 
    Vector3 NetForceAtPoint(Vector3 point)
    {
        Vector3 netCharge = Vector3.zero;


        foreach (Charge charge in Charges)
        {
            netCharge += charge.GetForceAtPoint(point);
        }
        return netCharge;
    }
    float NetPotentialAtPoint(Vector3 point)
    {
        float netCharge = 0;

        foreach (Charge charge in Charges)
        {
            netCharge += charge.GetPotentialAtPoint(point);
        }
        return netCharge;
    }


}
