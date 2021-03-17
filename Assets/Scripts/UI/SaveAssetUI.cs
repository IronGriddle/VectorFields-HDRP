using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveAssetUI : MonoBehaviour
{
    //For use in inspector
    public GameObject objectWithVectorField;
    
    public void SaveVectorField()
    {
        VectorField vectorField = objectWithVectorField.GetComponent<VectorField>();
        if (vectorField == null)
        {
            Debug.LogWarning("Cannot save non-instantiated Vector Field");
            return;
        }

        int name = Random.Range(0, 10000000);
        AssetDatabase.CreateAsset(vectorField.texture3D, "Assets/Textures/Texture3D/"+ name +".asset");
    }

    //For use in UI
    public void SaveVectorField(GameObject objectWithVectorField)
    {
        VectorField vectorField = objectWithVectorField.GetComponent<VectorField>();
        if (vectorField == null)
        {
            Debug.LogWarning("Cannot save non-instantiated Vector Field");
            return;
        }

        int name = Random.Range(0, 10000000);
        AssetDatabase.CreateAsset(vectorField.texture3D, "Assets/Textures/Texture3D/" + name + ".asset");
    }

    //For use elsewhere.
    public void SaveVectorField(VectorField vectorField)
    {
        if (vectorField == null)
        {
            Debug.LogWarning("Cannot save non-instantiated Vector Field");
            return;
        }

        int name = Random.Range(0,10000000);
        AssetDatabase.CreateAsset(vectorField.texture3D, "Assets/Textures/Texture3D/" + name + ".asset");
    }



}
