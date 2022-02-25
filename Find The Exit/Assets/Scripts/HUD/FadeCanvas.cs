using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeCanvas : MonoBehaviour
{
    private CanvasGroup group;

    [SerializeField] public float duration = 1.0f;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    public void FadeInUI()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeOutUI()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            group.alpha = elapsedTime / duration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        group.alpha = 1.0f;
    }


    private IEnumerator FadeOut()
    {

        float elapsedTime = duration;

        while (elapsedTime > 0)
        {
            group.alpha = elapsedTime / duration;
            elapsedTime -= Time.deltaTime;
            yield return null;
        }
        group.alpha = 0.0f;
    }

}
