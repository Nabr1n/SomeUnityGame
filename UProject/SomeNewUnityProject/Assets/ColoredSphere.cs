using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class ColoredSphere : MonoBehaviour
{
    // Start is called before the first frame update
    
   
    [SerializeField] Animation myAnim;
    [SerializeField] GameObject[] GameObjectsToChangeColor;
    private Color MyColor;

    public const float ColorAlpha = 0.1411765f;
    [SerializeField] Material materialToSet;


    void Start()
    {
        myAnim.Play();
    }

    void Update()
    {
        foreach (GameObject obj in GameObjectsToChangeColor)
        {
            //Material mat = obj.GetComponent<MeshRenderer>().sharedMaterial;
            obj.GetComponent<MeshRenderer>().sharedMaterial = new Material (materialToSet);
            obj.GetComponent<MeshRenderer>().sharedMaterial.color = MyColor;
            obj.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", MyColor);
        }
    }

    public void UpdateMyColor(Color newColor){

        MyColor = new Color (newColor.r, newColor.g, newColor.b, ColorAlpha);

    }

}
