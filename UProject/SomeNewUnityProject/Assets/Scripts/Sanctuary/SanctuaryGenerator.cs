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
    public int MyLevel;
    public GameObject ObjectToMoveWithPlayer;
    public bool StartMoving;
    [SerializeField] private Animation myAnimation;
    public GameObject MyDirectionalLight;
    private bool ReadyToGoOut = false;

    [SerializeField] private CapsuleCollider myCharCollider;
    [SerializeField] private GameObject mySun;

    [SerializeField] private GameObject SanctuaryPH;
    private Vector3 placeToMove;
    [SerializeField] private Transform playertransformtomove;
    //private GameObject[] PlaceHolders;
    [SerializeField] private float radius = 2f;
    
    [SerializeField] private List<CheckedSecret> SecretsCheck = new List<CheckedSecret>();

    public void OnMySpawn(int Level)
    {
        MyLevel = Level;
        PlaceHolders();
        Debug.Log(Level);
        GlobalSettings.GameGlobalSettings.SpawnTips(Level);
        myCharCollider.enabled = false;
        mySun.SetActive(false);
        if(Level == 1){
            for (int i = 0; i < GlobalSettings.GameGlobalSettings.FirstLevelSecrets.Count; i++)
            {
                SecretsCheck.Add(new CheckedSecret(GlobalSettings.GameGlobalSettings.FirstLevelSecrets[i].ColorString, false));
            }
        }
        else if(Level == 2){
            for (int i = 0; i < GlobalSettings.GameGlobalSettings.SecondLevelSecrets.Count; i++)
            {
                SecretsCheck.Add(new CheckedSecret(GlobalSettings.GameGlobalSettings.SecondLevelSecrets[i].ColorString, false));
            }   
        }
    }


    private void PlaceHolders(){
        
        int count = 1;
        switch(MyLevel){
            case(1):
            count = GlobalSettings.GameGlobalSettings.FirstLevelSecretsCount;
            break;
            case 2:
            count = GlobalSettings.GameGlobalSettings.SecondLevelSecretsCount;
            break;
        } 
        float current_angle = 0f;
        float additional_angle = 360f/(count*1f);

        //Debug.Log(additional_angle);


        for (int i = 0; i < count; i++)
        {
            float x = radius*Mathf.Cos(current_angle * Mathf.Deg2Rad) + gameObject.transform.position.x;
            float z = radius*Mathf.Sin(current_angle * Mathf.Deg2Rad) + gameObject.transform.position.z;

            GameObject newPH = Instantiate(SanctuaryPH, new Vector3(x, gameObject.transform.position.y, z), Quaternion.identity);
            newPH.transform.LookAt(this.gameObject.transform);
            switch (MyLevel){
                case 1:
                newPH.GetComponent<SantuaryKeyHolder>().mySecret = GlobalSettings.GameGlobalSettings.FirstLevelSecrets[i];
                break;
                case 2:
                newPH.GetComponent<SantuaryKeyHolder>().mySecret = GlobalSettings.GameGlobalSettings.SecondLevelSecrets[i];
                break;
            }
            
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
            other.gameObject.transform.parent = ObjectToMoveWithPlayer.transform;
            myAnimation.Play();
           
        }
    }
    

    void Update()
    {
        if(StartMoving){
            ObjectToMoveWithPlayer.transform.position -= new Vector3 (0, 1f * Time.deltaTime, 0);
            RaycastHit hit;
            Physics.Raycast(ObjectToMoveWithPlayer.transform.position + new Vector3(0, -1.3f, 0), new Vector3(0,-1, 0),  out hit);
            if(hit.distance<0.05f){
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
                MyDirectionalLight.SetActive(false);
                StartMoving = false;
            }
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
    void Start()
    {
        MyDirectionalLight = GlobalSettings.GameGlobalSettings.MainLight;
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
