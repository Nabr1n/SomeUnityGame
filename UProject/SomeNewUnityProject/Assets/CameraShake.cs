using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    public IEnumerator Shake(float magnitude, float duration){
        float currenttime = 0;
        Vector3 OriginalPosition = transform.localPosition;
        
        
        while (currenttime<duration){
            currenttime+=Time.deltaTime;
            float x = Random.Range(-1f,1f)*magnitude;
            float y = Random.Range(-1f, 1f)*magnitude;

            transform.localPosition = new Vector3 (x, y, OriginalPosition.z);
            yield return null;
        }
        transform.localPosition = OriginalPosition;

    }
}
