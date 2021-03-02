using B83.ExpressionParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function3D : MonoBehaviour
{
    //Should any error occur, it will be accessible from here.
    public string Error = "";


    //Possible parameters, eg f(t) or f(x,y,z)
    //Default is t x y z. Should be more than enough. Unused parameters will be ignored.
    //If a specified parameter does not exist in an equation it will be ignored.
    //Thus, f(t) and f(x,y,z) can be used.
    public List<string> parameters = new List<string> {"x", "y", "z"};

    public ExpressionDelegate xExpr;
    public ExpressionDelegate yExpr;
    public ExpressionDelegate zExpr;

    private void Awake()
    {
        SetExprX("z");
        SetExprZ("x");
        SetExprY("y");
    }



    public void SetExprX(string expression)
    {
        try
        {
            xExpr = Expression.Parse(expression).ToDelegate(parameters.ToArray());
        }
        catch (ExpressionParser.ParseException exception)//Should an error occur, set the expression equal to 0.
        {
            Error = exception.ToString();
            xExpr = Expression.Parse("x").ToDelegate(parameters.ToArray());
        }
    }

    public void SetExprY(string expression)
    {
        try
        {
            yExpr = Expression.Parse(expression).ToDelegate(parameters.ToArray());
        }
        catch (ExpressionParser.ParseException exception)
        {
            Error = exception.ToString();
            yExpr = Expression.Parse("x").ToDelegate(parameters.ToArray());
        }
    }

    public void SetExprZ(string expression)
    {
        try
        {
            zExpr = Expression.Parse(expression).ToDelegate(parameters.ToArray());
        }
        catch (ExpressionParser.ParseException exception) 
        {
            Error = exception.ToString();
            zExpr = Expression.Parse("x").ToDelegate(parameters.ToArray());
        }
    }

    //Calculates at a number.
    public Vector3 CalculateAtT(float t)
    {
        return new Vector3((float)xExpr(t), (float)yExpr(t), (float)zExpr(t));
    }

    //Calculates at a position.
    public Vector3 CalculateAtPosition(Vector3 position)
    {
       return new Vector3((float)xExpr(position.x,position.y,position.z), (float)yExpr(position.x, position.y, position.z), (float)zExpr(position.x, position.y, position.z));
    }

}
