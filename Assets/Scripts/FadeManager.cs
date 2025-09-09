using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public static class FadeManager
{
    public static event Action OnFinishFadeIn;
    public static event Action OnFinishFadeOut;

    public static void ResetEvents()
    {
        OnFinishFadeIn = null;
        OnFinishFadeOut = null;
    }

    // FADE IN
    public static IEnumerator FadeIN(Image fadeImage, float fadeSpeed)
    {
        float timeElapsed = 0;
        Color color = fadeImage.color;
        color.a = 0;

        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(0, 1, timeElapsed);
            fadeImage.color = color;
            yield return new WaitForSeconds(1 / fadeSpeed);
        }

        OnFinishFadeIn?.Invoke();
    }
    public static IEnumerator FadeIN(Image fadeImage, float fadeSpeed, float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        float timeElapsed = 0;
        Color color = fadeImage.color;
        color.a = 0;

        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(0, 1, timeElapsed);
            fadeImage.color = color;
            yield return new WaitForSeconds(1 / fadeSpeed);
        }

        OnFinishFadeIn?.Invoke();
    }
    public static IEnumerator FadeIN(TextMeshProUGUI fadeText, float fadeSpeed)
    {
        float timeElapsed = 0;
        Color color = fadeText.color;
        color.a = 0;

        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(0, 1, timeElapsed);
            fadeText.color = color;
            yield return new WaitForSeconds(1 / fadeSpeed);
        }
    }
    public static IEnumerator FadeIN(TextMeshProUGUI fadeText, float fadeSpeed, float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        float timeElapsed = 0;
        Color color = fadeText.color;
        color.a = 0;

        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(0, 1, timeElapsed);
            fadeText.color = color;
            yield return new WaitForSeconds(1 / fadeSpeed);
        }
    }

    // FADE OUT
    public static IEnumerator FadeOUT(Image fadeImage, float fadeSpeed)
    {
        float timeElapsed = 0;
        Color color = fadeImage.color;
        color.a = 1;

        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(1, 0, timeElapsed);
            fadeImage.color = color;
            yield return new WaitForSeconds(1 / fadeSpeed);
        }

        OnFinishFadeOut?.Invoke();
    }
    public static IEnumerator FadeOUT(Image fadeImage, float fadeSpeed, float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        float timeElapsed = 0;
        Color color = fadeImage.color;
        color.a = 1;

        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(1, 0, timeElapsed);
            fadeImage.color = color;
            yield return new WaitForSeconds(1 / fadeSpeed);
        }

        OnFinishFadeOut?.Invoke();
    }
    public static IEnumerator FadeOUT(TextMeshProUGUI fadeText, float fadeSpeed)
    {
        float timeElapsed = 0;
        Color color = fadeText.color;
        color.a = 1;

        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(1, 0, timeElapsed);
            fadeText.color = color;
            yield return new WaitForSeconds(1 / fadeSpeed);
        }
    }
    public static IEnumerator FadeOUT(TextMeshProUGUI fadeText, float fadeSpeed, float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        float timeElapsed = 0;
        Color color = fadeText.color;
        color.a = 1;

        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Lerp(1, 0, timeElapsed);
            fadeText.color = color;
            yield return new WaitForSeconds(1 / fadeSpeed);
        }
    }
}