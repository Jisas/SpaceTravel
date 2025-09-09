using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LensDistortionManager : MonoBehaviour
{
    [SerializeField] private VolumeProfile volume;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float effectSpeed = 1;

    public delegate void ImageFullyVisibleAction();
    public static event ImageFullyVisibleAction OnEffectAwait;
    public delegate void FinishEffect();
    public static event FinishEffect OnEffectFinish;

    private LensDistortion lensDistortion;
    private const float startFadeInIntensity = 0.0f;
    private const float startFadeInScale = 1.0f;
    private const float startFadeOutIntensity = -1.0f;
    private const float startFadeOutScale = 0.01f;

    private void Start()
    {
        if (volume.TryGet(out LensDistortion temp))
        {
            lensDistortion = temp;
        }
    }
    public void ResetEvents()
    {
        OnEffectAwait = null;
        OnEffectFinish = null;
    }
    private IEnumerator FadeEffect(float awaitTime)
    {
        float timeElapsed = 0;

        // Fade in intensity
        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * effectSpeed;
            lensDistortion.intensity.Override(Mathf.Lerp(startFadeInIntensity, startFadeOutIntensity, timeElapsed));
            yield return null;
        }

        timeElapsed = 0;

        // Fade in scale and image alpha
        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * effectSpeed;
            lensDistortion.scale.Override(Mathf.Lerp(startFadeInScale, startFadeOutScale, timeElapsed));
            Color color = fadeImage.color;
            color.a = timeElapsed;
            fadeImage.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(awaitTime);
        OnEffectAwait?.Invoke();
        timeElapsed = 0;

        // Fade out scale and image alpha
        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * effectSpeed;
            lensDistortion.scale.Override(Mathf.Lerp(startFadeOutScale, startFadeInScale, timeElapsed));
            Color color = fadeImage.color;
            color.a = 1 - timeElapsed;
            fadeImage.color = color;
            yield return null;
        }

        timeElapsed = 0;

        // Fade out intensity
        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * effectSpeed;
            lensDistortion.intensity.Override(Mathf.Lerp(startFadeOutIntensity, startFadeInIntensity, timeElapsed));
            yield return null;
        }

        OnEffectFinish?.Invoke();
    }  
    public void StartCompleteEffect(float awaitTime) => StartCoroutine(FadeEffect(awaitTime));
    public IEnumerator StartEnterEffect()
    {
        float timeElapsed = 0;

        // Fade in intensity
        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * effectSpeed;
            lensDistortion.intensity.Override(Mathf.Lerp(startFadeInIntensity, startFadeOutIntensity, timeElapsed));
            yield return null;
        }

        timeElapsed = 0;

        // Fade in scale and image alpha
        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * effectSpeed;
            lensDistortion.scale.Override(Mathf.Lerp(startFadeInScale, startFadeOutScale, timeElapsed));
            Color color = fadeImage.color;
            color.a = timeElapsed;
            fadeImage.color = color;
            yield return null;
        }

        OnEffectFinish?.Invoke();
    }
    public IEnumerator StartExitEffect()
    {
        float timeElapsed = 0;

        // Fade out scale and image alpha
        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * effectSpeed;
            lensDistortion.scale.Override(Mathf.Lerp(startFadeOutScale, startFadeInScale, timeElapsed));
            Color color = fadeImage.color;
            color.a = 1 - timeElapsed;
            fadeImage.color = color;
            yield return null;
        }

        timeElapsed = 0;

        // Fade out intensity
        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * effectSpeed;
            lensDistortion.intensity.Override(Mathf.Lerp(startFadeOutIntensity, startFadeInIntensity, timeElapsed));
            yield return null;
        }

        OnEffectFinish?.Invoke();
    }
}