using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SanctuaryGenerator : MonoBehaviour
{
    [System.Serializable]
    private class CheckedSecret{
        public string ColorCode;
        public bool Exist;
        public int Count = 0;
        public CheckedSecret (string colorcode, bool exist){
            ColorCode = colorcode;
            Exist = exist;
        }
    }


    [SerializeField] private GameObject SanctuaryPH;
    //private GameObject[] PlaceHolders;
    [SerializeField] private float radius = 2f;
    
    [SerializeField] private List<CheckedSecret> SecretsCheck = new List<CheckedSecret>();

    private void Start()
    {
        PlaceHolders();
        GlobalSettings.GameGlobalSettings.SpawnTips();
        
        for (int i = 0; i < GlobalSettings.GameGlobalSettings.FirstLevelSecrets.Count; i++)
        {
            SecretsCheck.Add(new CheckedSecret(GlobalSettings.GameGlobalSettings.FirstLevelSecrets[i].ColorString, false));
        }
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
            newPH.GetComponent<SantuaryKeyHolder>().myGenerator = this;
            
            //Debug.Log (GlobalSettings.GameGlobalSettings.FirstLevelSecrets[i]);

            current_angle += additional_angle;
        }
    }
    
    public void AddColor(string code){
        bool set = false;
        for (int i = 0; i < SecretsCheck.Count; i++)
        {
            if(code == SecretsCheck[i].ColorCode&&!SecretsCheck[i].Exist){
                SecretsCheck[i].Exist = true;
                SecretsCheck[i].Count++;
                Debug.Log("NEW COLOR!");
                set = true;
                break;
            }
        }
        if(!set){
              for (int i = 0; i < SecretsCheck.Count; i++)
              {
                if(code == SecretsCheck[i].ColorCode&&SecretsCheck[i].Exist){
                Debug.Log("EXISTING COLOR ++!");
                SecretsCheck[i].Count++;
                
            }
              }
        }
    }

    public void RemoveColor(string code){
        bool removedwithoutlost = false;
          for (int i = 0; i < SecretsCheck.Count; i++)
        {
            if (code == SecretsCheck[i].ColorCode && SecretsCheck[i].Exist && SecretsCheck[i].Count > 1){
                SecretsCheck[i].Count --;
                removedwithoutlost = true;
                break;
            }
        }
        if(!removedwithoutlost){
            for (int i = 0; i < SecretsCheck.Count; i++)
            {
                if (code == SecretsCheck[i].ColorCode && SecretsCheck[i].Exist && SecretsCheck[i].Count > 0)
                {
                    SecretsCheck[i].Count = 0;
                    SecretsCheck[i].Exist = false;
                }
            }
        }
    }
}
