using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalSettings : MonoBehaviour
{
    public int FirstLevelSecretsCount, SecondLevelSecretsCount;
    public List<GameObject> ClosedSanctuaryFloors1,ClosedSanctuaryFloors2  = new List<GameObject>();
    public List<GameObject> SanctuaryFloors1,SanctuaryFloors2 = new List<GameObject>();

    public List<GameObject> NotSanctuaryFloors1, NotSanctuaryFloors2 = new List<GameObject>();

    public static GlobalSettings GameGlobalSettings;

    private ColorMath myColorMath = new ColorMath();

    public List<string> AllBasicColorsToBePlaced1, AllBasicColorsToBePlaced2 = new List<string>();

    public List<GameObject> AllCellsWithBlobsFirstLevel, AllCellsWithBlobsSecondLevel = new List<GameObject>();


    public SecretColor[] AllSecretColors;

    public List<SecretColor> FirstLevelSecrets, SecondLevelSecrets;
    public bool FirsLevelGenerated = false;
    public GameObject TipPrefab;

    [HideInInspector]public float FloorsSize;

    public bool ShouldSpawnedInClosedSanctuary;

    void Start()
    {
        GameGlobalSettings = this;
        
        FirstLevelSecrets = GenerateSecrets(FirstLevelSecretsCount);
        SecondLevelSecrets = GenerateSecrets(SecondLevelSecretsCount);

        AllBasicColorsToBePlaced1 = CalculateAllNeededColors(FirstLevelSecrets);
        AllBasicColorsToBePlaced2 = CalculateAllNeededColors(FirstLevelSecrets);
        // while(true){
        //     if(FirsLevelGenerated){
        //         SpawnColorBlobs(AllBasicColorsToBePlaced, NotSanctuaryFloors);
        //         break;
        //         }
        // }
        
    }

    public List<SecretColor> GenerateSecrets(int Count){
        List<SecretColor> secrets = new List<SecretColor>();
        for (int i = 0; i < Count; i++)
        {
            secrets.Add(GetUnlockedColor(secrets));
        }

        return secrets;





    }


    private List<string> CalculateAllNeededColors(List<SecretColor> secretColors){
        List<string> returnList = new List<string>();

        for (int i = 0; i < secretColors.Count; i++)
        {
            for (int k = 0; k < myColorMath.GetBasicColorsFromMixed(secretColors[i].ColorString).Count; k++)
            {
                returnList.Add(myColorMath.GetBasicColorsFromMixed(secretColors[i].ColorString)[k]);
            }
        }




        return returnList;

    }


     public void SpawnColorBlobs(List<string> colors, List<GameObject> floors){
         List<GameObject> checkedFloors = new List<GameObject>();

         for (int i = 0; i < colors.Count; i++)
         {
             while (true){
                int Randint = Random.Range (0, floors.Count);
                if (!checkedFloors.Contains(floors[Randint])){
                    Debug.Log("NEW BLOB, step: " + i + " Randint: " + Randint );
                    checkedFloors.Add(floors[Randint]);
                    break;
                }
             }
         }
        
        for (int i = 0; i < checkedFloors.Count; i++)
        {
            checkedFloors[i].GetComponent<FloorScript>().CheckBlob(true, AllBasicColorsToBePlaced1[i]);
        }


     }

    public SecretColor GetUnlockedColor(List<SecretColor> lockedcolors){
        SecretColor checkedColor = AllSecretColors[0];
        while (true)
        {
            int randint = Random.Range(0, AllSecretColors.Length);
            checkedColor = AllSecretColors[randint];
            bool found = false;
            for (int k = 0; k < lockedcolors.Count; k++)
            {
                if (lockedcolors[k].ColorString == checkedColor.ColorString){
                    found = true;
                    break;
                }
            }
            if (!found) break;
        }

        return checkedColor;

    }

    public void SpawnTipsInClosedSanctuary(int Level){
        List<int> lockedfloors = new List<int>();
        switch (Level){
        case (1):
        for (int i = 0; i <FirstLevelSecretsCount; i++)
        {
            int floorindex;
            while (true){
                floorindex = Random.Range(0,ClosedSanctuaryFloors1.Count);
                if (!lockedfloors.Contains(floorindex)) break;
            }
            lockedfloors.Add(floorindex);
            RaycastHit hit;
            if (Physics.Raycast(ClosedSanctuaryFloors1[floorindex].transform.position + new Vector3 (0, 0.5f, 0), new Vector3 (0, 3f, 0), out hit)){
                

                float randX = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                float randZ = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                Instantiate (TipPrefab, hit.transform.position + new Vector3(randX, 0, randZ), Quaternion.identity);
                Debug.DrawRay (ClosedSanctuaryFloors1[floorindex].transform.position + new Vector3 (0, 3f, 0) + new Vector3(randX, 0, randZ), new Vector3 (0,-50f, 0), Color.red, 150f);
            }
            
            
        }
        break;
        case (2):
        for (int i = 0; i <FirstLevelSecretsCount; i++)
        {
            int floorindex;
            while (true){
                floorindex = Random.Range(0,ClosedSanctuaryFloors2.Count);
                if (!lockedfloors.Contains(floorindex)) break;
            }
            lockedfloors.Add(floorindex);
            RaycastHit hit;
            if (Physics.Raycast(ClosedSanctuaryFloors2[floorindex].transform.position + new Vector3 (0, 0.5f, 0), new Vector3 (0, 3f, 0), out hit)){
                

                float randX = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                float randZ = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                SanctuaryTip NewTip = Instantiate (TipPrefab, hit.transform.position + new Vector3(randX, 0, randZ), Quaternion.identity).GetComponent<SanctuaryTip>();
                Debug.DrawRay (ClosedSanctuaryFloors2[floorindex].transform.position + new Vector3 (0, 3f, 0) + new Vector3(randX, 0, randZ), new Vector3 (0,-50f, 0), Color.red, 150f);
            }
            
            
        }
        break;
    }
    }

    public void SpawnTipsInFullSanctuary1(int Level){
        List<int> lockedfloors = new List<int>();
           switch (Level){
            case(1):
            
                for (int i = 0; i <FirstLevelSecretsCount; i++)
                {
                    Debug.Log(i + "1");
                    
                    int floorindex;
                    while (true){
                        floorindex = Random.Range(0,SanctuaryFloors1.Count);
                        if (!lockedfloors.Contains(floorindex)) break;
                    }
                    lockedfloors.Add(floorindex);
                    RaycastHit hit;
                    if (Physics.Raycast(SanctuaryFloors1[floorindex].transform.position + new Vector3 (0, 3f, 0), new Vector3 (0, -50f, 0), out hit)){
                        GameObject T;
                        
                        float randX = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                        float randZ = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                        T = Instantiate (TipPrefab, hit.transform.position + new Vector3(randX, 0, randZ), Quaternion.identity);
                        T.GetComponent<SanctuaryTip>().SetMySecret(FirstLevelSecrets[i]);
                        T.GetComponent<SanctuaryTip>().MyLevel = 1;
                        Debug.DrawRay (SanctuaryFloors1[floorindex].transform.position + new Vector3 (0, 3f, 0) + new Vector3(randX, 0, randZ), new Vector3 (0,-50f, 0), Color.red, 150f);
                    }
                }
            break;
            case (2):
                for (int i = 0; i <SecondLevelSecretsCount; i++)
                {
                    Debug.Log(i + "2");

                    int floorindex;
                    while (true){
                        floorindex = Random.Range(0,ClosedSanctuaryFloors2.Count);
                        if (!lockedfloors.Contains(floorindex)) break;
                    }
                    lockedfloors.Add(floorindex);
                    RaycastHit hit;
                    if (Physics.Raycast(SanctuaryFloors2[floorindex].transform.position + new Vector3 (0, 0.3f, 0), new Vector3 (0, -15f, 0), out hit)){
                        GameObject T;

                        float randX = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                        float randZ = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                        T = Instantiate (TipPrefab, hit.transform.position + new Vector3(randX, 0, randZ), Quaternion.identity);
                        T.GetComponent<SanctuaryTip>().SetMySecret(SecondLevelSecrets[i]);
                        T.GetComponent<SanctuaryTip>().MyLevel = 2;
                        Debug.DrawRay (SanctuaryFloors2[floorindex].transform.position + new Vector3 (0, 0.1f, 0) + new Vector3(randX, 0, randZ), new Vector3 (0,-50f, 0), Color.red, 150f);
                    }
                
                
                }
            break;
           }

            
        }
    

    public void SpawnTips (int level){
        switch (level){
        case (1):
            if (ShouldSpawnedInClosedSanctuary) 
            {
                
                SpawnTipsInClosedSanctuary(1);
            }
            else{
                
                SpawnTipsInFullSanctuary1(1);
            }
        break;
        case(2):
            if (ShouldSpawnedInClosedSanctuary) 
            {
                
                SpawnTipsInClosedSanctuary(2);
            }
            else{
                
                SpawnTipsInFullSanctuary1(2);
            }
            break;
    }
    }


}

