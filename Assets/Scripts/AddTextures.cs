using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTextures : MonoBehaviour 
{
    private GameObject[] objectsToApplyMaterialTo;

    void Start()
    {
        SetMaterials("Concrete pattern 06", "BuildingMaterial");
    }

    void SetMaterials(string materialName, string targetTag)
    {
        Material newMat = Resources.Load(materialName, typeof(Material)) as Material;
        objectsToApplyMaterialTo = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obj in objectsToApplyMaterialTo)
        {
            obj.GetComponent<Renderer>().material = newMat;
        }
    }
}
