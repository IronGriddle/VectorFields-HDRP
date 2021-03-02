using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
public class pCacheExporter : MonoBehaviour
{
     
    //What the string may look like for a size of 2:
    //
    //pcache
    //format ascii 1.0
    //comment Exported with PCache.cs
    //elements 2
    //property float position.x
    //property float position.y
    //property float position.z
    //property float velocity.x
    //property float velocity.y
    //property float velocity.z
    //end_header
    //-0.3562704 0.07376606 0.3401374 -0.715848 0.1467688 0.6826569
    //-0.1122971 -0.0249265 0.4830243 -0.22657 -0.05720714 0.9723134

    static string GetHeader(int elements)
    {
        return
            "pcache\n" +
            "format ascii 1.0\n" +
            "elements" + elements + "\n" +           //setting element count here.
            "property float position.x\n" +
            "property float position.y\n" +
            "property float position.z\n" +
            "property float velocity.x\n" +
            "property float velocity.y\n" +
            "property float velocity.z\n" +
            "end_header\n";
    }

    public static string GetString(List<Vector3> positions, List<Vector3> velocities)
    {
        //Input Checking Positions must equal velocities.
        if (positions.Count != velocities.Count)
        { 
            throw new System.ArgumentException("positions.Count != velocities.Count");
        }

        //Creating Header.
        string header = GetHeader(positions.Count);
        string body = "";

        //Generating body
        for (int i = 0; i < positions.Count; i++)
        {
            body += positions[i].x;
            body += positions[i].y;
            body += positions[i].z;

            body += velocities[i].x;
            body += velocities[i].y;
            body += velocities[i].z;
            body += "\n";
        }

        return header + body;
    }

    //Save in ASCII Format.
    public static void SavePCacheString(string pCache)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(pCache);
    }








}
