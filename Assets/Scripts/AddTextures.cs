using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTextures : MonoBehaviour 
{
    private GameObject[] objectsToApplyMaterialTo;

    void Start()
    {
        objectsToApplyMaterialTo = GameObject.FindGameObjectsWithTag("BuildingMaterial");
        foreach(GameObject obj in objectsToApplyMaterialTo)
        {
            SetMaterialsOfChildren(obj.transform, "Concrete pattern 06");
        }
    }

    void SetMaterialsOfChildren(Transform obj, string materialName)
    {
        if(obj.childCount == 0)
        {   //Set the material for the child
            obj.GetComponent<Renderer>().material = Resources.Load(materialName, typeof(Material)) as Material;
        }
        else //Keep looking for the children recursively
        {
            int numChildren = obj.childCount;
            print("Still children");
            foreach(Transform child in obj)
            {
                SetMaterialsOfChildren(child, materialName);
            }
        }
    }
}
