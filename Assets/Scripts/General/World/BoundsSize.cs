using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsSize : MonoBehaviour
{
    public Bounds bounds;
    public Bounds paddedBounds;
    public float padding = 4f;

    // Start is called before the first frame update
    void Awake()
    {
        //initializing bounds to render with a decent size.
        bounds.min = -Vector3.one*2;
        bounds.max = Vector3.one*2;
        UpdatePaddingSize();
        UpdateCubeSize();
    }

    public void SetPaddingSize(float newPaddingSize)
    {
        padding = newPaddingSize;
    }

    void UpdatePaddingSize()
    {
        paddedBounds = bounds;
        paddedBounds.min = bounds.min - Vector3.one * padding;
        paddedBounds.max = bounds.max + Vector3.one * padding;
    }

    //gets the transform to match the padded bounds.
    void UpdateCubeSize()
    {
        StartCoroutine(LerpPosition(paddedBounds.center, 0.5f)) ;
        StartCoroutine(LerpTransform(paddedBounds.size + Vector3.one * 1.01f, 0.5f)); //1.01f is for clipping Vector3.one is due to .contain not rounding up.
    }

    //TODO: Pull all lerps into a lerp utility class.
    bool lerpPosIsRunning = false;
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        if (lerpPosIsRunning)
        {
            yield return null;
        }
        lerpPosIsRunning = true;

        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        lerpPosIsRunning = false;

        yield return null;
    }

    bool lerpTransformIsRunning = false;
    IEnumerator LerpTransform(Vector3 targetScale, float duration)
    {
        //pass if instance is running
        if (lerpTransformIsRunning)
        {
            yield return null;
        }

        lerpTransformIsRunning = true;
        float time = 0;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
        lerpTransformIsRunning = false;

        yield return null;
    }

    //ACCESSED FROM EDITOR
    public void _OnPlace(Vector3Int position)
    {
        bounds.Encapsulate(position);

        UpdatePaddingSize();
        UpdateCubeSize();
    }

    public void _OnDestroy()
    {
        GameObject[] charges = GameObject.FindGameObjectsWithTag("Charge");

        bounds = new Bounds(); //Resetting bounds.
        foreach (var charge in charges) //Growing bounds to accomodate for all internal positions.
        {
            bounds.Encapsulate(charge.transform.position);
        }

        UpdatePaddingSize();
        UpdateCubeSize();
    }

    public void UpdateBounds()
    {
        UpdatePaddingSize();
        UpdateCubeSize();

    }


}
