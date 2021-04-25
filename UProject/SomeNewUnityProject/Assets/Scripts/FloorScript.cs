using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public bool AmIMainRoad;
   [SerializeField] GameObject myPlane;
    public GameObject WallLeft;
    public GameObject WallBottom;

    [SerializeField] private GameObject[] Floors;
    [SerializeField] private Transform floortransform;


    [HideInInspector] public bool MazeExit;

    public void SetRoad(bool IsMainRoad){
        AmIMainRoad = IsMainRoad;
        if(AmIMainRoad)
            myPlane.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    

    private void PickRandomFloor(){
        int Rand = Random.Range(0, Floors.Length-1);
        Instantiate(Floors[Rand], floortransform);
    }


    void Start()
    {
        PickRandomFloor();
    }

    // Update is called once per frame
    void Update()
    {
        if (MazeExit) myPlane.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
