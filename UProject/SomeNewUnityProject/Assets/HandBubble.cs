using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBubble : MonoBehaviour
{
    // Start is called before the first frame update
    public BlobObject CurrentBlob;
    private MeshRenderer myRend;
    public GameObject sphere;
    private void Start() {
        myRend = sphere.GetComponent<MeshRenderer>();
    }




    // Update is called once per frame
    void Update()
    {
        myRend.material = CurrentBlob.myMaterial;
    }
}
