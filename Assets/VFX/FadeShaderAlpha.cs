using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeShaderAlpha : MonoBehaviour
{
    Material material;
    // Start is called before the first frame update
    void Awake()
    {
        Renderer Renderer = gameObject.GetComponent<Renderer>();
        material = Renderer.material;
    }

    void SetAlpha(float a)
    {
        material.SetFloat("_Alpha", a);
    }

    float GetAlpha()
    {
        return material.GetFloat("_Alpha");
    }

    public void Fade()
    {
        StartCoroutine(FadeToZero());
    }
    public void Show()
    {
        StartCoroutine(FadeToOne());
    }
    



    IEnumerator FadeToZero()
    {
        float timeElapsed = 0;
        float lerpDuration = 0.5f;
        float alpha = GetAlpha();
        float alphaInitial = alpha;
        float endAlpha = 0;

        while (timeElapsed < lerpDuration)
        {
            alpha = Mathf.Lerp(alphaInitial, endAlpha, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;


            SetAlpha(alpha);


            yield return null;
        }
        alpha = endAlpha;

        SetAlpha(alpha);

    }



    IEnumerator FadeToOne()
    {
        float timeElapsed = 0;
        float lerpDuration = 0.5f;
        float alpha = GetAlpha();
        float alphaInitial = alpha;
        float endAlpha = 1;

        while (timeElapsed < lerpDuration)
        {
            alpha = Mathf.Lerp(alphaInitial, endAlpha, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            SetAlpha(alpha);

            yield return null;
        }
        alpha = endAlpha;

        SetAlpha(alpha);

    }
}
