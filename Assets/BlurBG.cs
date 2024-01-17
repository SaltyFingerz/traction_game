using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurBG : MonoBehaviour
{

    public PostProcessVolume ppVol;

    void Update()
    {
        DepthOfField dph;
        if (ppVol.profile.TryGetSettings<DepthOfField>(out dph))
        {
            dph.active = true;
        }
    }


}
