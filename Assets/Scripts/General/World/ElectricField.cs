using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectricField : MonoBehaviour
{
    Charge[] Charges;

    //RGB corresponds to the field
    //A corresponds to potential at that point
    public Texture3D fieldTexture;
    public int count; //number of pixels stored inside of fieldTexture. Created from bounds.
    Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        UpdateChargeList();
    }

    public float updatePeriod = 4f;
    public bool updateRunning = true;
    IEnumerator DelayedUpdate()
    {
        while (updateRunning)
        {
            UpdateChargeList();
            SetTextureFromBounds();
            count = GetNCalculated();
            yield return new WaitForSeconds(updatePeriod);
        }
    }

    public int GetNCalculated()
    {
        BoundsInt boundsInt = WorldGrid.RoundedBounds(bounds);
        int count = 0;
        foreach (var item in boundsInt.allPositionsWithin)
        {
            count++;
        }
        return count;
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

    public void SetTextureFromBounds()
    {
        BoundsInt boundsInt = WorldGrid.RoundedBounds(bounds);

        //Setting texture 3d bounds. Note that zMax - zMin is in the middle parameter due to computer based coordinates.
        Texture3D texture = new Texture3D(boundsInt.xMax - boundsInt.xMin, boundsInt.zMax - boundsInt.zMin, boundsInt.yMax - boundsInt.yMin, TextureFormat.RGBAFloat, true);

        foreach (Vector3Int position in boundsInt.allPositionsWithin)
        {
            Vector3 force = NetForceAtPoint(position);
            float potential = NetPotentialAtPoint(position);
            Color dataPixel = new Color(force.x,force.y,force.z, potential);
            texture.SetPixel(position.x, position.y, position.z, dataPixel);
        }
        texture.Apply(true);

        fieldTexture = texture;
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

    //It might be better to do this with alpha culling in unity vfx graph.
    Vector3Int[] GetPositionsAtPotential(float potential, float threshold)
    {
        if (fieldTexture == null)
            return null;

        List<Vector3Int> positions = new List<Vector3Int>();
        BoundsInt boundsInt = WorldGrid.RoundedBounds(bounds);


        foreach (Vector3Int checkedPosition in boundsInt.allPositionsWithin)
        {
            Color color = fieldTexture.GetPixel(checkedPosition.x, checkedPosition.y, checkedPosition.z);
            
            if (color.a < threshold && threshold > color.a)
            {
                positions.Add(checkedPosition);
            }
        
        }
        return positions.ToArray();
    }


}
