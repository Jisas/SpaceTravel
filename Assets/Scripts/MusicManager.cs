using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource Source { get; private set; }

    void Start()
    {
        Source = GetComponent<AudioSource>();
        Source.volume = 0.0f;

        if (SceneManager.GetActiveScene().name == "Menu")
             SetAudioClip(Resources.Load<AudioClip>("Music/BattleMusic"));
        else SetAudioClip(Resources.Load<AudioClip>("Music/CosmicVoyage"));

    }
    void Update()
    {
        if (Source != null && !Source.isPlaying)
        {
            Source.Play();
            StartCoroutine(Fade(true, Source, 3, 1));
        }
    }

    public void SetAudioClip(AudioClip clip)
    {
        Source.clip = clip;
    }
    public void Play(float fadeInDuration = 0)
    {
        Source.Play();
        StartCoroutine(Fade(true, Source, fadeInDuration, 1));
    }
    public void Stop(float fadeOutDuration = 0)
    {
        StartCoroutine(Fade(false, Source, fadeOutDuration, 0));
    }

    private IEnumerator Fade(bool isFadeIn, AudioSource source, float duration, float targetVolume)
    {
        if (!isFadeIn)
        {
            double audioLength = (double)source.clip.length / source.clip.frequency;
            yield return new WaitForSecondsRealtime((float)audioLength - duration);
        }

        float time = 0;
        float startVol = source.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            yield return null;
        }

        yield break;
    }
}
