using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function3DTester : MonoBehaviour
{
    public Function3D function3D;
    int radiusOfCube = 10;
    const bool SUCCESS = true;
    const bool FAILURE = false;

    

    // Start is called before the first frame update
    void Start()
    {
        //Getting Function 3D;
        function3D = gameObject.GetComponent<Function3D>();
        if (function3D == null)
        {
            gameObject.AddComponent<Function3D>();
        }
        Debug.Log("Testing Function 3D - Bounds: " + radiusOfCube);
        TestXYZ();
        TestVortex();
        GenerateVortexPCache();
    }

    public void SavePCache(List<Vector3> positions, List<Vector3> forces)
    {
        PCacheCustom pCache = new PCacheCustom();
        pCache.AddVector3Property("position");
        pCache.AddVector3Property("velocity");
        pCache.SetVector3Data("position", positions);
        pCache.SetVector3Data("velocity", forces);
        pCache.SaveToFile("Assets/pointCache.pCache", PCacheCustom.Format.Ascii);

    }
    private void TestXYZ()
    {
        //Setting Function 3D;
        function3D.SetExprX("x");
        function3D.SetExprY("y");
        function3D.SetExprZ("z");

        List<bool> tests = new List<bool>();
        for (float z = -radiusOfCube; z < radiusOfCube; z++)
        {
            for (float y = -radiusOfCube; y < radiusOfCube; y++)
            {
                for (float x = -radiusOfCube; x < radiusOfCube; x++)
                {
                    Vector3 v1 = function3D.CalculateAtVector3(new Vector3(x, y, z));
                    Vector3 v2 = XYZ_Expression(x, y, z);
                    tests.Add(vector3EqualityEstimate(v1, v2, 10));
                }
            }
        }

        int failedTests = 0;
        foreach (var item in tests)
        {
            if (item == false)
            {
                failedTests++;
            }
        }

        Debug.Log("XYZ Equation | Failed Tests: " + failedTests);
    }

    private void GenerateXYZPCache()
    {
        List<Vector3> positions = new List<Vector3>();
        List<Vector3> forces = new List<Vector3>();

        for (float z = -radiusOfCube; z < radiusOfCube; z++)
        {
            for (float y = -radiusOfCube; y < radiusOfCube; y++)
            {
                for (float x = -radiusOfCube; x < radiusOfCube; x++)
                {
                    positions.Add(new Vector3(x, y, z));
                    forces.Add(function3D.CalculateAtVector3(XYZ_Expression(x, y, z)));
                }
            }
        }
        SavePCache(positions, forces);
    }

    private void GenerateVortexPCache()
    {
        List<Vector3> positions = new List<Vector3>();
        List<Vector3> forces = new List<Vector3>();

        for (float z = -radiusOfCube; z < radiusOfCube; z++)
        {
            for (float y = -radiusOfCube; y < radiusOfCube; y++)
            {
                for (float x = -radiusOfCube; x < radiusOfCube; x++)
                {
                    positions.Add(new Vector3(x,y,z));
                    forces.Add(function3D.CalculateAtVector3(VortexExpression(x, y, z)));
                }
            }
        }
        SavePCache(positions, forces);
    }

    private void TestVortex()
    {
        //Setting Function 3D;
        function3D.SetExprX("-y/(x^2+y^2)");
        function3D.SetExprY("x/(x^2+y^2)");
        function3D.SetExprZ("0");


        List<bool> tests = new List<bool>();
        for (float z = -radiusOfCube; z < radiusOfCube; z++)
        {
            for (float y = -radiusOfCube; y < radiusOfCube; y++)
            {
                for (float x = -radiusOfCube; x < radiusOfCube; x++)
                {
                    Vector3 v1 = function3D.CalculateAtVector3(new Vector3(x, y, z));
                    Vector3 v2 = VortexExpression(x, y, z);
                    tests.Add(vector3EqualityEstimate(v1, v2, 50));
                }
            }
        }

        int failedTests = 0;
        foreach (var item in tests)
        {
            if (item == false)
            {
                failedTests++;
            }
        }
        Debug.Log("Vortex Equation | Failed Tests: " + failedTests);
    }


    //Expressions typed in C#
    private Vector3 VortexExpression(float x, float y, float z)
    {
        return new Vector3(
            (-y) / (Mathf.Pow(x,2) + Mathf.Pow(y,2)),
            x / (Mathf.Pow(x, 2) + Mathf.Pow(y, 2)),
            0
            );
    }

    private Vector3 XYZ_Expression(float x, float y,float z)
    {
        return new Vector3(x, y, z);
    }


    bool floatEqual(float f1, float f2, float percentTolerance = 5)
    {
        //Calculating percent error and checking if it passes tolerance.
        if (Mathf.Abs((f1 - f2)/f2)*100 < percentTolerance)
        {
            return SUCCESS;
        }
        else
        {
            return FAILURE;
        }
    }

    bool vector3EqualityEstimate(Vector3 v1, Vector3 v2, float percentTolerance)
    {
        //TODO: THIS THING EXISTS FUTURE ME
        if (Vector3.Distance(v1,v2) < 0.05f)
        {
            return SUCCESS;
        }

        if (floatEqual(v1.x, v2.x, percentTolerance) && (floatEqual(v1.y, v2.y, percentTolerance)) && floatEqual(v1.z, v2.z, percentTolerance))
        {
            return SUCCESS;
        }
        else { return FAILURE; }
    }

}
