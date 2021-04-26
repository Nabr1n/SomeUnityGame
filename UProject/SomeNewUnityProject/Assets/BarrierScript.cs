using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider myCollision;
    public ParticleSystem [] myParticles;

    public GameObject ParticleHolder;

    public BlobObject myKeyBlob;

    private bool AmIActive;

    private void Start() {
            AmIActive = true;
            ParticleHolder.SetActive(true);
            myCollision.enabled = true;
            for (int i = 0; i < myParticles.Length; i++)
            {
                myParticles[i].startColor = myKeyBlob.MyColor;
            }    
    }
    private void Update() {
        if(ColorBubbleInventory.ActivatedBlob!=null){ 
            if(ColorBubbleInventory.ActivatedBlob.MyColorName == myKeyBlob.MyColorName){
            AmIActive = false;
            ParticleHolder.SetActive(false);
            myCollision.enabled = false;

        }
        else{
            if(!AmIActive){
                AmIActive = true;
            ParticleHolder.SetActive(true);
            myCollision.enabled = true;
            for (int i = 0; i < myParticles.Length; i++)
            {
                myParticles[i].startColor = myKeyBlob.MyColor;
            }

            }
        }
        }
    }




}
