using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Old class made way back.
public class FunctionEval : MonoBehaviour
{
    //function takes in a float, and outputs a float.
    public static float EvalAt(Func<float, float> method, float x)
    {
        return method(x);
    }


    //function takes in two floats, and outputs a float.
    public static float EvalAt(Func<float, float, float> method, float x, float y)
    {
        return method(x, y);
    }

    //function takes in three floats, and outputs a float.
    public static float EvalAt(Func<float, float, float, float> method, float x, float y, float z)
    {
        return method(x, y, z);
    }



    //takes in function and a range, outputs a list of points to be used in the line render when I get that set up.
    //probably will require objects of the functions.

    //Point1 = what is the X that the function will be evaluated from?
    //Point2 = what is the x that the function will be evaluated to?
    //resolution = how many times do I want to evaluate the function between the x's?
    //TODO: maybe make resolution scale based on the distance of x's;


    public static List<Vector3> FuncEvalsOneVariable(Func<float, float> method, float resolution, float x1, float x2)
    {

        //float x1 = Mathf.Min(ix1, ix2);
       // float x2 = Mathf.Max(ix1, ix2);

        float distanceBetween = Mathf.Abs(x1 - x2);
        float segmentLength = distanceBetween / resolution;
        float currentSegmentLength = 0;

        List<Vector3> functionPoints = new List<Vector3>();

        for (int i = 0; i <= resolution; i++)
        {
            float currentX = currentSegmentLength + Mathf.Min(x1,x2);
            currentSegmentLength += segmentLength;

            float y = method(currentX);


            Vector3 Point = new Vector3(currentX, y);
            functionPoints.Add(Point);

        }
        
        return functionPoints;
    }


    public static Vector2 MidPoint(Vector2 pointA, Vector2 pointB)
    {
        Vector2 MidPoint = new Vector2((pointA.x + pointB.x) / 2, (pointA.y + pointB.y) / 2);
        return MidPoint;
    }


    public static float LengthBetweenPoints(Vector2 pointA, Vector2 pointB)
    {
        float distance = Mathf.Sqrt((pointA.x - pointB.x) * (pointA.x - pointB.x) + (pointA.y - pointB.y) * (pointA.y - pointB.y));

        return distance;
    }



}
