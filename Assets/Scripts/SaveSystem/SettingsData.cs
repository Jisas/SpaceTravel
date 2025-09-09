
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    [Header("Controls")]
    public bool gyroInUse;
    public float sensitivity;

    [Header("Screen")]
    public int frameRate;
    public float cameraFOV;
    public float brightness;
    public int quality;
    public bool antiAliasing;

    [Header("Sound")]
    public float generalVolume;
    public float musicVolume;
    public float effectsVolume;
}
