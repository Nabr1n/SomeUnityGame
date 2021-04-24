﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public bool AmIMainRoad;
   [SerializeField] GameObject myPlane;


    public void SetRoad(bool IsMainRoad){
        AmIMainRoad = IsMainRoad;
        if(AmIMainRoad)
            myPlane.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
