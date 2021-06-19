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
        Debug.Log(code + ":    " + color);
        
        myObjectWithSphere.SetActive(true);
        myObjectWithSphere.GetComponent<ColoredSphere>().UpdateMyColor(color);

        myGenerator.AddColor(code);

        
        



    }
    
}
