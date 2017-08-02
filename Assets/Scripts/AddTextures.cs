using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTextures : MonoBehaviour 
{
    void Start()
    {
        SetMaterial("Concrete6", "Concrete textures pack/pattern 06/Concrete pattern 06");
        SetMaterial("Concrete7", "Concrete textures pack/pattern 07/Concrete pattern 07");
    }

    void SetMaterialsOfChildren(Transform obj, string materialName)
    {
        if(obj.childCount == 0)
        {   //Set the material for the child
            obj.GetComponent<Renderer>().material = Resources.Load(materialName, typeof(Material)) as Material;
        }
        else //Keep looking for the children recursively
        {
            foreach(Transform child in obj)
            {
                SetMaterialsOfChildren(child, materialName);
            }
        }
    }

    void SetMaterial(string tagName, string materialPath)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagName);
        foreach (GameObject obj in taggedObjects)
        {
            SetMaterialsOfChildren(obj.transform, materialPath);
        }
    }
}
