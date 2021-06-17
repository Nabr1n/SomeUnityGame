using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctuaryTip : MonoBehaviour
{
    public GameObject myHolder;
    public SecretColor mySecret;

    public void SetMySecret(SecretColor mysecret){
        mySecret = mysecret;
        myHolder.GetComponent<MeshRenderer>().material = mySecret.Mesh.GetComponent<Renderer>().sharedMaterial;
         myHolder.GetComponent<MeshFilter>().mesh = mySecret.Mesh.GetComponent<MeshFilter>().sharedMesh;
    }

    
}
