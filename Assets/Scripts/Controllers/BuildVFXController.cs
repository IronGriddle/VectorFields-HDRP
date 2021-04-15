using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BuildVFXController : MonoBehaviour
{
    VisualEffect vfx;
    HighlighDestruction highlighDestruction;
    HighlightPlacement highlightPlacement;

    // Start is called before the first frame update
    void Awake()
    {
        vfx = gameObject.GetComponent<VisualEffect>();
        highlighDestruction = gameObject.GetComponentInChildren<HighlighDestruction>();
        highlightPlacement = gameObject.GetComponentInChildren<HighlightPlacement>();
    }


    public void DisableBuildVFX()
    {
        vfx.Stop();
        highlighDestruction.enabled = false;
        highlightPlacement.enabled = false;
    }

    public void EnableBuildVFX()
    {
        vfx.Play();
        highlighDestruction.enabled = true;
        highlightPlacement.enabled = true;
    }

}
