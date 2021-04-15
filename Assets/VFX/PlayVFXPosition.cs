using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayVFXPosition : MonoBehaviour
{
    VisualEffect vfx;
    // Start is called before the first frame update
    void Start()
    {
        vfx = gameObject.GetComponent<VisualEffect>();
    }

    // Accessed via editor
    public void Play(Vector3 position)
    {
        transform.position = position;
        vfx.Play();
    }
    public void Play(Vector3Int position)
    {
        transform.position = position;
        vfx.Play();
    }

    public void Stop(Vector3 position)
    {
        transform.position = position;
        vfx.Stop();
    }
    public void Stop(Vector3Int position)
    {
        transform.position = position;
        vfx.Stop();
    }
    public void Stop()
    {
        vfx.Stop();
    }
}
