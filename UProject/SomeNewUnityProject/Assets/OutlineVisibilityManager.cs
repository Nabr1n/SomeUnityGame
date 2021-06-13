using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineVisibilityManager : MonoBehaviour
{
    public GameObject myOutlineObject;



    private void Start() {
        myOutlineObject.GetComponent<Outline>().enabled = false;
    }
}
