using B83.ExpressionParser;
using System.Collections.Generic;
using UnityEngine;

public class Function3D : MonoBehaviour
{

    //Should any error occur during parsing., it will be accessible from here.
    public string Error = "";


    //Possible parameters, eg f(t) or f(x,y,z)
    //Default is x y z. Should be more than enough. Unused parameters will be ignored.
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


    //Set the xExpr delegate with the proper string. eg : "sin(x) + cos(y)"
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

    //Set the yExpr delegate with the proper string. eg : "sin(x) + cos(y)"
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

    //Set the zExpr delegate with the proper string. eg : "sin(x) + cos(y)"
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
    //Given that the number of parameters defined is one. Eg: f(x) 
    public Vector3 CalculateAtT(float t)
    {
        if (parameters.Count == 1)
        {
            return new Vector3((float)xExpr(t), (float)yExpr(t), (float)zExpr(t));
        }
        else
        {
            Debug.LogWarning("CalculateAtT() is calculating an expression with more than one parameter and will calculate t for the first parameter.  did you mean to use CalculateAtPosition?");
            return new Vector3((float)xExpr(t), (float)yExpr(t), (float)zExpr(t));
        }
    }

    //Calculates at a position.
    //Given that the number of parameters defined is three. Eg: f(x,y,z)
    public Vector3 CalculateAtPosition(Vector3 position)
    {
        if (parameters.Count == 3)
        {
            return new Vector3((float)xExpr(position.x, position.y, position.z), (float)yExpr(position.x, position.y, position.z), (float)zExpr(position.x, position.y, position.z));
        }
        else
        {
            Debug.LogWarning("CalculateAtPosition is calculating an expression with more or less than 3 parameters and will try calculate for the first three parameters. Did you mean to use CalculateAtT()?");
            return new Vector3((float)xExpr(position.x, position.y, position.z), (float)yExpr(position.x, position.y, position.z), (float)zExpr(position.x, position.y, position.z));
        }
    }

}
