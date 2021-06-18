using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





[ExecuteAlways]
public class WidgetSwitcher : MonoBehaviour
{
    [SerializeField] private int CurrentWidgetIndex;
    [SerializeField] private List<GameObject> Widgets;
    private int lastUpdateIndex = 0;
    

    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    public void SetActiveWidgetIndex(int index){
        CurrentWidgetIndex = index;
    }


    private void UpdateWidgetIndex(int currentindex){
        for (int i = 0; i < Widgets.Count; i++)
        {
            Widgets[i].SetActive(false);
        }
        Widgets[CurrentWidgetIndex].SetActive(true);
    }

    public int GetActiveWidgetIndex(){
        return CurrentWidgetIndex;
    }


    // Update is called once per frame
    void Update()
    {
        if (lastUpdateIndex!=CurrentWidgetIndex){
            UpdateWidgetIndex(CurrentWidgetIndex);
            lastUpdateIndex = CurrentWidgetIndex;
        }
    }
}
