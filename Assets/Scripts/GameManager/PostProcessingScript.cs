using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingScript : MonoBehaviour
{
    [SerializeField] private PostProcessVolume _postProcessVolume; 
    public void turnPostProcessingOnOf()
    {
        if(!GameManager.postProcessingIsOn)
        {
            _postProcessVolume.isGlobal = true;
            GameManager.postProcessingIsOn = true;

        }
        else if(GameManager.postProcessingIsOn)
            {
                _postProcessVolume.isGlobal = false;
                GameManager.postProcessingIsOn = false;
            }
    }

    private void FixedUpdate() 
    {
        if(GameManager.postProcessingIsOn)
        {
            _postProcessVolume.isGlobal = true;
            GameManager.postProcessingIsOn = true;
        }
        else if(!GameManager.postProcessingIsOn)
            {
                _postProcessVolume.isGlobal = false;
                GameManager.postProcessingIsOn = false;
            }
    }
}
