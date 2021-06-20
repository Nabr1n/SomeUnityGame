using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenOpener : MonoBehaviour
{
    [SerializeField] private bool ShouldOpenfullScreen;
    [SerializeField] private Vector2Int Resolution;
    void Start()
    {
        if(ShouldOpenfullScreen){
            Screen.SetResolution(Resolution.x, Resolution.y, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
