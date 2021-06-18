using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantuaryKeyHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public SecretColor mySecret;
    public GameObject myObjectWithSphere;
    public SanctuaryGenerator myGenerator;



    void Start()
    {
        myObjectWithSphere.SetActive(false);
    }

    public void AddMyBlob(string code, Color color){
        //Debug.Log(code + ":    " + color);
        
        myObjectWithSphere.SetActive(true);
        Material material = myObjectWithSphere.GetComponent<MeshRenderer>().sharedMaterial;
        myObjectWithSphere.GetComponent<MeshRenderer>().material = new Material(material);
        myObjectWithSphere.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);

        myGenerator.AddColor(code);

        
        



    }
    
}
