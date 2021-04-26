using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundedPBScript : MonoBehaviour
{
    [SerializeField] private Image Indicator;
    [SerializeField] private Image Bar;

    [SerializeField] private string mySide;
    private ColorBubbleInventory myInv;

    public void UpdateBar(float CurrentNum, float MaxNum){
        float alpha = CurrentNum/MaxNum;
        Bar.fillAmount = alpha;
    }

    private void Start() {
        myInv = GameObject.FindWithTag("Player").GetComponent<ColorBubbleInventory>();
    }


    private void Update() {
        switch (mySide){
            case "Left":
            UpdateBar(myInv.LMBcurrent, myInv.LMBMax);
            

            break;
            
            case "Right":
            UpdateBar(myInv.RMBcurrent, myInv.RMBMax);


            break;
        }
    }

}
