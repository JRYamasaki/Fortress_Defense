using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTextures : MonoBehaviour 
{
    private GameObject[] objectsToApplyMaterialTo;

    void Start()
    {
        Material newMat = Resources.Load("Concrete pattern 06", typeof(Material)) as Material;
        objectsToApplyMaterialTo = GameObject.FindGameObjectsWithTag("BuildingMaterial");

        foreach (GameObject obj in objectsToApplyMaterialTo)
        {
            obj.GetComponent<Renderer>().material = newMat;
        }

        //GameObject structure = GameObject.FindWithTag("Struct1");
        //structure.GetComponent<Renderer>().material = newMat;
    }
}
