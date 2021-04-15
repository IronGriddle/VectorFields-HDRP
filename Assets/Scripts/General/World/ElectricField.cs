using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectricField : MonoBehaviour
{
    Charge[] Charges;

    //RGB corresponds to the field
    //A corresponds to potential
    public Texture3D fieldTexture;
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
            yield return new WaitForSeconds(updatePeriod);
        }
    }

    public void UpdateChargeList()
    {
        //dummy variable used in null checking.
        Charge[] charges = FindObjectsOfType<Charge>();
        if (charges.Length != 0)
        {
            Charges = charges;
        }

    }

    public void SetTextureFromBounds()
    {
        BoundsInt boundsInt = new BoundsInt(WorldGrid.RoundedPoint(bounds.min), WorldGrid.RoundedPoint(bounds.max));
        Texture3D texture = new Texture3D(boundsInt.xMax - boundsInt.xMin, boundsInt.zMax - boundsInt.zMin, boundsInt.yMax - boundsInt.yMin, TextureFormat.RGBAFloat, true);

        foreach (Vector3Int position in boundsInt.allPositionsWithin)
        {
            Vector3 force = NetForceAtPoint(position);
            float potential = NetPotentialAtPoint(position);
            Color dataPixel = new Color(force.x,force.y,force.z, potential);
            texture.SetPixel(position.x, position.y, position.z, dataPixel);
        }

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



}
