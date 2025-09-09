using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioMixer m_SoundMixer;
    private GameManager m_GameManager;

    void Start()
    {
        m_GameManager = GameManager.Instance;
        m_SoundMixer = Resources.Load<AudioMixer>("Settings/SoundMixer");

        SetGeneralVolume();
        SetMusicVolume();
        SetEffectsVolume();
    }

    public void SetGeneralVolume()
    {
        var generalVolume = Mathf.Log10(m_GameManager.GetSettingsData().generalVolume) * 20;
        m_SoundMixer.SetFloat("Volume", generalVolume);

        AudioListener.pause = (m_GameManager.GetSettingsData().generalVolume <= 0);
    }

    public void SetMusicVolume()
    {
        var musicVolume = Mathf.Log10(m_GameManager.GetSettingsData().musicVolume) * 20;
        m_SoundMixer.SetFloat("MusicVolume", musicVolume);
    }

    public void SetEffectsVolume()
    {
        var effectsVolume = Mathf.Log10(m_GameManager.GetSettingsData().effectsVolume) * 20;
        m_SoundMixer.SetFloat("EffectsVolume", effectsVolume);
    }
}
