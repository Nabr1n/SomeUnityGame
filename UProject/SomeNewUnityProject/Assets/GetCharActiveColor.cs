using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetCharActiveColor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText;

    // Update is called once per frame
    void Update()
    {
        myText.SetText(GameObject.FindWithTag("Player").GetComponent<ColorBubbleInventory>().myActivatedColor);
    }
}
