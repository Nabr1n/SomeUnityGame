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

    [SerializeField] private Animation myAnimation;

    private bool ReadyToGoOut = false;

    [SerializeField] private CapsuleCollider myCharCollider;
    [SerializeField] private GameObject mySun;

    [SerializeField] private GameObject SanctuaryPH;
    [SerializeField] private Transform playertransformtomove;
    //private GameObject[] PlaceHolders;
    [SerializeField] private float radius = 2f;
    
    [SerializeField] private List<CheckedSecret> SecretsCheck = new List<CheckedSecret>();

    private void Start()
    {
        PlaceHolders();
        GlobalSettings.GameGlobalSettings.SpawnTips();
        myCharCollider.enabled = false;
        mySun.SetActive(false);
        
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
    
    void OnTriggerEnter(Collider other)
    {
        if(ReadyToGoOut&other.gameObject.CompareTag("Player")){
            other.gameObject.transform.position = playertransformtomove.position+ new Vector3(0, 0.5f, 0);
            other.gameObject.GetComponent<PlayerMovement>().enabled = false;
            myAnimation.Play();
        }
    }
    




    public void AddColor(string code){
        bool set = false;
        bool allExist = true;
        for (int i = 0; i < SecretsCheck.Count; i++)
        {
            if(code == SecretsCheck[i].ColorCode&&!SecretsCheck[i].Exist){
                SecretsCheck[i].Exist = true;
                SecretsCheck[i].Count++;
                //Debug.Log("NEW COLOR!");
                set = true;
                break;
            }
        }
        if(!set){
              for (int i = 0; i < SecretsCheck.Count; i++)
              {
                if(code == SecretsCheck[i].ColorCode&&SecretsCheck[i].Exist){
                //Debug.Log("EXISTING COLOR ++!");
                SecretsCheck[i].Count++;
                
            }
              }
        }
        for (int i = 0; i < SecretsCheck.Count; i++)
        {
            if(!SecretsCheck[i].Exist) 
            {
                allExist = false;
                break;
            }
        }
        if(allExist) StartCheckingForExit();

    }



    public void StartCheckingForExit(){
        myCharCollider.enabled = true;
        mySun.SetActive(true);
        ReadyToGoOut = true;
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
