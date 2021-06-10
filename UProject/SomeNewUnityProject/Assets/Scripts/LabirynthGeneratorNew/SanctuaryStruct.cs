using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "MyObjects/SecretColor")]
[System.Serializable]
public class SecretColor: ScriptableObject
{
    public string MyTip;
    public string ColorString;
    public Color MyColor;
}