using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GlobalSettings : MonoBehaviour
{
    public int FirstLevelSecretsCount;
    public List<GameObject> ClosedSanctuaryFloors = new List<GameObject>();
    public List<GameObject> SanctuaryFloors = new List<GameObject>();

    public static GlobalSettings GameGlobalSettings;

    public SecretColor[] AllSecretColors;

    public List<SecretColor> FirstLevelSecrets;
    public GameObject TipPrefab;

    [HideInInspector]public float FloorsSize;

    public bool ShouldSpawnedInClosedSanctuary;

    void Start()
    {
        GameGlobalSettings = this;
        
        FirstLevelSecrets = GenerateSecrets(FirstLevelSecretsCount);
    }

    public List<SecretColor> GenerateSecrets(int Count){
        List<SecretColor> secrets = new List<SecretColor>();
        for (int i = 0; i < Count; i++)
        {
            secrets.Add(GetUnlockedColor(secrets));
        }

        return secrets;





    }

    public SecretColor GetUnlockedColor(List<SecretColor> lockedcolors){
        SecretColor checkedColor = AllSecretColors[0];
        for (int i = 0; i < AllSecretColors.Length; i++)
        {
            checkedColor = AllSecretColors[i];
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

    public void SpawnTipsInClosedSanctuary(){
        List<int> lockedfloors = new List<int>();
        for (int i = 0; i <FirstLevelSecretsCount; i++)
        {
            int floorindex;
            while (true){
                floorindex = Random.Range(0,ClosedSanctuaryFloors.Count);
                if (!lockedfloors.Contains(floorindex)) break;
            }
            lockedfloors.Add(floorindex);
            RaycastHit hit;
            if (Physics.Raycast(ClosedSanctuaryFloors[floorindex].transform.position + new Vector3 (0, 3f, 0), new Vector3 (0, -50f, 0), out hit)){
                

                float randX = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                float randZ = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                Instantiate (TipPrefab, hit.transform.position + new Vector3(randX, 0, randZ), Quaternion.identity);
                Debug.DrawRay (ClosedSanctuaryFloors[floorindex].transform.position + new Vector3 (0, 3f, 0) + new Vector3(randX, 0, randZ), new Vector3 (0,-50f, 0), Color.red, 150f);
            }
            
            
        }
    }

    public void SpawnTipsInFullSanctuary(){
        List<int> lockedfloors = new List<int>();
        for (int i = 0; i <FirstLevelSecretsCount; i++)
        {
            int floorindex;
            while (true){
                floorindex = Random.Range(0,SanctuaryFloors.Count);
                if (!lockedfloors.Contains(floorindex)) break;
            }
            lockedfloors.Add(floorindex);
            RaycastHit hit;
            if (Physics.Raycast(SanctuaryFloors[floorindex].transform.position + new Vector3 (0, 3f, 0), new Vector3 (0, -50f, 0), out hit)){
                GameObject T;
                
                float randX = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                float randZ = Random.Range(-1*(FloorsSize-(FloorsSize*0.1f)), FloorsSize-(FloorsSize*0.1f)) / 2f;
                T = Instantiate (TipPrefab, hit.transform.position + new Vector3(randX, 0, randZ), Quaternion.identity);
                T.GetComponent<SanctuaryTip>().mySecret = FirstLevelSecrets[i];
                Debug.DrawRay (SanctuaryFloors[floorindex].transform.position + new Vector3 (0, 3f, 0) + new Vector3(randX, 0, randZ), new Vector3 (0,-50f, 0), Color.red, 150f);
            }
            
            
        }
    }

    public void SpawnTips (){
        if (ShouldSpawnedInClosedSanctuary) SpawnTipsInClosedSanctuary();
        else SpawnTipsInFullSanctuary();
    }


}

