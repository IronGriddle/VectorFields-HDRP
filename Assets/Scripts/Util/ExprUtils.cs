using B83.ExpressionParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExprUtils : MonoBehaviour
{
    public static float numDerivative(float t, ExpressionDelegate expressionDelegate) //Calculate the numerical derivative with 0.001.
    {
        return (float)((expressionDelegate(0.001 + t) - expressionDelegate(t - 0.001)) / (2 * 0.001));
    }

    public static Vector3 numDerivative3D(Vector3 vector, Function3D delegate3D)
    {
        return new Vector3(numDerivative(vector.x, delegate3D.xExpr), numDerivative(vector.y, delegate3D.yExpr), numDerivative(vector.z, delegate3D.zExpr));
    }

    public static Vector3 numDerivative3D(float t, Function3D delegate3D)
    {
        return new Vector3(numDerivative(t, delegate3D.xExpr), numDerivative(t, delegate3D.yExpr), numDerivative(t, delegate3D.zExpr));
    }

}
