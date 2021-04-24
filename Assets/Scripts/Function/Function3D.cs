using B83.ExpressionParser;
using System.Collections.Generic;
using UnityEngine;

public class Function3D : MonoBehaviour
{

    //Should any error occur during parsing., it will be accessible from here.
    public string Error = "";
    public string[] parameters;

    ExpressionParser parser = new ExpressionParser();

    //Some weird memory thing going on.
    private ExpressionDelegate reset;
    public ExpressionDelegate xExpr;
    public ExpressionDelegate yExpr;
    public ExpressionDelegate zExpr;

    public void Awake()
    {
        parameters = new string[] {"x", "y", "z"};


        reset = Expression.Parse("0").ToDelegate(parameters);
        //Default vortex.
        SetExprX("(-y)/(x^2+y^2)");
        SetExprY("(x)/(x^2+y^2)");
        SetExprZ("1/(x^2+y^2+z^2)");
    }

    //Set the xExpr delegate with the proper string. eg : "sin(x) + cos(y)"
    public void SetExprX(string expression)
    {
        try
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = "0";
            }

            xExpr = reset;
            xExpr = Expression.Parse(expression).ToDelegate(parameters);
        }
        catch (ExpressionParser.ParseException exception) 
        {
            Error = exception.ToString();
        }
    }

    //Set the yExpr delegate with the proper string. eg : "sin(x) + cos(y)"
    public void SetExprY(string expression)
    {
        try
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = "0";
            }
            yExpr = reset;
            yExpr = Expression.Parse(expression).ToDelegate(parameters);
        }
        catch (ExpressionParser.ParseException exception)
        {
            Error = exception.ToString();
        }
    }

    //Set the zExpr delegate with the proper string. eg : "sin(x) + cos(y)"
    public void SetExprZ(string expression)
    {
        try
        {
            if (string.IsNullOrEmpty(expression) || string.IsNullOrWhiteSpace(expression))
            {
                expression = "0";
            }

            zExpr = reset;
            zExpr = Expression.Parse(expression).ToDelegate(parameters);
        }
        catch (ExpressionParser.ParseException exception)
        {
            Error = exception.ToString();
        }
    }




    //Calculates at a number.
    //Given that the number of parameters defined is one. Eg: f(x) 

    //Calculates at a position given that the number of parameters is three. Eg: f(x,y,z)
    public Vector3 CalculateAtVector3(Vector3 position)
    {
        double x = position.x;
        double y = position.y;
        double z = position.z;


        double[] param = new double[] { x, y, z };

        float xValue = (float)xExpr(param);
        float yValue = (float)yExpr(param);
        float zValue = (float)zExpr(param);


        return new Vector3(xValue, yValue, zValue);


    }
}
