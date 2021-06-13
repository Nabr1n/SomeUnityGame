using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctuaryGenerator : MonoBehaviour
{
    [SerializeField] private GameObject SanctuaryPH;
    //private GameObject[] PlaceHolders;
    [SerializeField] private float radius = 2f;

    private void Start()
    {
        PlaceHolders();
        GlobalSettings.GameGlobalSettings.SpawnTips();
    }


    private void PlaceHolders(){
        
        int count = GlobalSettings.GameGlobalSettings.FirstLevelSecretsCount;
        float current_angle = 0f;
        float additional_angle = 360f/(count*1f);

        //Debug.Log(additional_angle);


        for (int i = 0; i < count; i++)
        {
            float x = radius*Mathf.Cos(current_angle * Mathf.Deg2Rad) + gameObject.transform.position.x;
            float z = radius*Mathf.Sin(current_angle * Mathf.Deg2Rad) + gameObject.transform.position.z;

            GameObject newPH = Instantiate(SanctuaryPH, new Vector3(x, gameObject.transform.position.y, z), Quaternion.identity);
            newPH.transform.LookAt(this.gameObject.transform);
            newPH.GetComponent<SantuaryKeyHolder>().mySecret = GlobalSettings.GameGlobalSettings.FirstLevelSecrets[i];
            Debug.Log (GlobalSettings.GameGlobalSettings.FirstLevelSecrets[i]);

            current_angle += additional_angle;
        }
    }
}
