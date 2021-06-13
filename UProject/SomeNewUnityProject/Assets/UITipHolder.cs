using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UITipHolder : MonoBehaviour
{
    public int myIndex;
    private GameObject Player;
    private SecretColor myTip;

    [SerializeField] private GameObject myImage, myText; 
    private TextMeshProUGUI Text;
    private RawImage Image;




    void Start()
    {
        
    }

    public void UpdateMe(int index){
        Text = myText.GetComponent<TextMeshProUGUI>();
        Image = myImage.GetComponent<RawImage>();
        myIndex = index;
        Debug.Log(myIndex);
        myTip = GameObject.FindWithTag("Player").GetComponent<ColorBubbleInventory>().TipsInventory[myIndex];
        Text.SetText(myTip.MyTip);
        Image.texture = myTip.myTexture;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
