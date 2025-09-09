using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
using MyBox;

#region Enums
public enum ControlType
{
    Slider,
    Selector
}
public enum SliderSettingsOptions
{
    Sensitivity = 0,
    CameraFOV = 1,
    Brightness = 2,
    GeneralVolume = 3,
    MusicVolume = 4,
    EffectsVolume = 5
}
public enum SelectorSettingsOptions
{
    GyroInUse = 0,
    FrameRate = 1,
    Quality = 2,
    Antialiasing = 3,
}
public enum InputOptions
{
    Joystick = 0,
    Gyroscope = 1,
}
public enum AntialiasingOptions
{
    Yes = 0,
    No = 1,
}
public enum FameRateOptions
{
    Rate30 = 0,
    Rate60 = 1,
    Rate120 = 2
}
#endregion

#region Other Class
[Serializable]
public class SettingsSelector
{
    public Button leftButton;
    public Button rightButton;
    public TextMeshProUGUI text;

    public SettingsSelector (Button leftButton, Button rightButton, TextMeshProUGUI text)
    {
        this.leftButton = leftButton;
        this.rightButton = rightButton;
        this.text = text;
    }
}

[Serializable]
public class SettingsSlider
{
    public Slider slider;
    public TextMeshProUGUI text;

    public SettingsSlider (Slider slider, TextMeshProUGUI text)
    {
        this.slider = slider;
        this.text = text;
    }
}
#endregion

public class UIControllDataSetter : MonoBehaviour
{
    [SerializeField] private ControlType controlType;

    [ConditionalField("controlType", false, ControlType.Slider), SerializeField]
    private SliderSettingsOptions sliderSettings;

    [ConditionalField("controlType", false, ControlType.Selector), SerializeField]
    private SelectorSettingsOptions selectorSettings;

    [Space(5)]

    [ConditionalField("controlType", false, ControlType.Selector), SerializeField]
    private SettingsSelector selector;

    [ConditionalField("controlType", false, ControlType.Slider), SerializeField]
    private SettingsSlider slider;

    private GameManager m_GameManager;
    private SoundManager m_SoundManager;
    private int optionIndex = 0;
    private int[] options;

    void Start()
    {
        m_GameManager = GameManager.Instance;
        m_SoundManager = FindAnyObjectByType<SoundManager>();

        switch (controlType)
        {
            case ControlType.Slider:
                SetupSlider();
                break;

            case ControlType.Selector:
                SetupSelector();
                break;
        }
    }
    void Update()
    {
        switch (controlType)
        {
            case ControlType.Slider:
                SetSettingsBySlider(slider.slider.value);
                break;

            case ControlType.Selector:
                SetSettingsBySelector();
                break;
        }

        m_GameManager.SaveSettingsData();
    }

    private void SetupSlider()
    {
        switch (sliderSettings)
        {
            case SliderSettingsOptions.Sensitivity:
                slider.slider.minValue = 0.1f;
                slider.slider.maxValue = 1.0f;
                slider.slider.value = m_GameManager.GetSettingsData().sensitivity;
                break;

            case SliderSettingsOptions.CameraFOV:
                slider.slider.minValue = 100f;
                slider.slider.maxValue = 120f;
                slider.slider.value = m_GameManager.GetSettingsData().cameraFOV;
                break;

            case SliderSettingsOptions.Brightness:
                slider.slider.minValue = 0.1f;
                slider.slider.maxValue = 1.0f;
                slider.slider.value = m_GameManager.GetSettingsData().brightness;
                break;

            case SliderSettingsOptions.GeneralVolume:
                slider.slider.minValue = 0.0f;
                slider.slider.maxValue = 1.0f;
                slider.slider.value = m_GameManager.GetSettingsData().generalVolume;
                break;

            case SliderSettingsOptions.MusicVolume:
                slider.slider.minValue = 0.0f;
                slider.slider.maxValue = 1.0f;
                slider.slider.value = m_GameManager.GetSettingsData().musicVolume;
                break;

            case SliderSettingsOptions.EffectsVolume:
                slider.slider.minValue = 0.0f;
                slider.slider.maxValue = 1.0f;
                slider.slider.value = m_GameManager.GetSettingsData().effectsVolume;
                break;
        }
    }
    private void SetSettingsBySlider(float value)
    {
        slider.text.text = Math.Round(value, 2).ToString();

        switch (sliderSettings)
        {
            case SliderSettingsOptions.Sensitivity:
                m_GameManager.SetSesitivity(value);
                break;

            case SliderSettingsOptions.CameraFOV:
                m_GameManager.SetCameraFOV(value);
                break;

            case SliderSettingsOptions.Brightness:
                m_GameManager.SetBrightness(value);
                Screen.brightness = value;
                break;

            case SliderSettingsOptions.GeneralVolume:
                m_GameManager.SetGeneralVolume(value);
                m_SoundManager.SetGeneralVolume();
                break;

            case SliderSettingsOptions.MusicVolume:
                m_GameManager.SetMusicVolume(value);
                m_SoundManager.SetMusicVolume();
                break;

            case SliderSettingsOptions.EffectsVolume:
                m_GameManager.SetEffectsVolume(value);
                m_SoundManager.SetEffectsVolume();
                break;
        }
    }

    private void SetupSelector()
    {
        switch (selectorSettings)
        {
            case SelectorSettingsOptions.GyroInUse:
                optionIndex = m_GameManager.GetSettingsData().gyroInUse == true ? 1 : 0;
                options = (int[])System.Enum.GetValues(typeof(InputOptions));
                selector.rightButton.onClick.AddListener(() => SelectorRightButton(options));
                selector.leftButton.onClick.AddListener(() => SelectorLeftButton(options));
                break;

            case SelectorSettingsOptions.FrameRate:
                optionIndex = m_GameManager.GetSettingsData().frameRate;
                options = (int[])System.Enum.GetValues(typeof(FameRateOptions));
                selector.rightButton.onClick.AddListener(() => SelectorRightButton(options));
                selector.leftButton.onClick.AddListener(() => SelectorLeftButton(options));
                break;

            case SelectorSettingsOptions.Quality:
                optionIndex = m_GameManager.GetSettingsData().quality;
                options = new int[QualitySettings.count];
                selector.rightButton.onClick.AddListener(() => SelectorRightButton(options));
                selector.leftButton.onClick.AddListener(() => SelectorLeftButton(options));
                break;

            case SelectorSettingsOptions.Antialiasing:
                optionIndex = m_GameManager.GetSettingsData().antiAliasing == true ? 0 : 1;
                options = (int[])System.Enum.GetValues(typeof(AntialiasingOptions));
                selector.rightButton.onClick.AddListener(() => SelectorRightButton(options));
                selector.leftButton.onClick.AddListener(() => SelectorLeftButton(options));
                break;
        }
    }
    private void SelectorRightButton(int[] options)
    {
        optionIndex = optionIndex >= options.Length ? 0 : (optionIndex + 1) % options.Length;
        SetSettingsBySelector();
    }
    private void SelectorLeftButton(int[] options)
    {
        optionIndex = optionIndex <= 0 ? options.Length - 1 : (optionIndex - 1) % options.Length;
        SetSettingsBySelector();
    }
    private void SetSettingsBySelector()
    {
        switch (selectorSettings)
        {
            case SelectorSettingsOptions.GyroInUse:
                selector.text.text = System.Enum.GetName(typeof(InputOptions), optionIndex);
                m_GameManager.SetGyroInUse(optionIndex == 1);
                break;

            case SelectorSettingsOptions.FrameRate:
                var temp = optionIndex == 0 ? 30 : optionIndex == 1 ? 60 : optionIndex == 2 ? 120 : 30;
                selector.text.text = $"{temp} FPS";
                m_GameManager.SetFrameRate(temp);
                Application.targetFrameRate = temp;
                break;

            case SelectorSettingsOptions.Quality:
                selector.text.text = QualitySettings.names[optionIndex];
                m_GameManager.SetQuality(optionIndex);
                QualitySettings.SetQualityLevel(optionIndex);
                break;

            case SelectorSettingsOptions.Antialiasing:
                selector.text.text = System.Enum.GetName(typeof(AntialiasingOptions), optionIndex);
                m_GameManager.SetAntiAliasing(optionIndex == 0);
                QualitySettings.antiAliasing = optionIndex == 0 ? 0: 2;
                break;
        }
    }
}