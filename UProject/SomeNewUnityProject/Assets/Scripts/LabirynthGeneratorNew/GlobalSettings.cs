using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GlobalSettings : MonoBehaviour
{
    public int FirstLevelSecretsCount;
    public List<GameObject> ClosedSanctuaryFloors = new List<GameObject>();

    public static GlobalSettings GameGlobalSettings;


    void Start()
    {
        GameGlobalSettings = this;
    }

}
