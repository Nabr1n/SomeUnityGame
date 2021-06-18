using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
public class UnderWaterTest : MonoBehaviour
{

    //This script enables underwater effects. Attach to main camera.

    //Define variable
    public int underwaterLevel = 7;

    public Texture2D fogGradient;

    //The scene's default fog settings
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private Material defaultSkybox;
    private Material noSkybox;

    private PostProcessVolume postProcess;
    private Bloom b;
    private Vignette vg;
    void Start()
    {
        //Set the background color
        GetComponent<Camera>().backgroundColor = new Color(0, 0.4f, 0.7f, 1); 
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        defaultSkybox = RenderSettings.skybox;

        postProcess = GetComponent<PostProcessVolume>();
        ChangeBloom(0.5f);
       
    }

    public void ChangeBloom(float newBloomValue)
    {
        Bloom bloom;
        if(postProcess.profile.TryGetSettings(out bloom))
        {
            bloom.intensity.value = newBloomValue;
        }

    }


    public void ChangeDepthOfField(float val)
    {
        DepthOfField depth;
        if (postProcess.profile.TryGetSettings(out depth))
        {
            depth.focusDistance.value = val;  //.intensity.value = newBloomValue;
        }

    }



    private void OldFogSettings(){
        if (transform.position.y < underwaterLevel)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
            RenderSettings.fogDensity = 0.04f;
            RenderSettings.skybox = noSkybox;
        }
        else
        {
            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            RenderSettings.skybox = defaultSkybox;
        }
    }


    void Update()
    {
        RenderSettings.fog = true;
        //RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
        RenderSettings.fogColor = fogGradient.GetPixel(underwaterLevel, 1);
        RenderSettings.fogDensity = 0.04f * underwaterLevel;
        RenderSettings.skybox = noSkybox;
    }
}