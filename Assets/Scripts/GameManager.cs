using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private PlayerData playerData;
    [Space(10)]
    [SerializeField] private SettingsData settingsData;
    public int GameStartsNum { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        LoadPlayerData();
        IncreaseGameNum();
        LoadSettingsData();
        ApplySettingsByData();
    }

    #region Player Data
    [ContextMenu("SavePlayerData")]
    public void SavePlayerData() => DataSerializer.SavePlayer(playerData);
    public void LoadPlayerData() => playerData = DataSerializer.LoadPlayer();
    public PlayerData GetPlayerData() => playerData;
    public void IncreaseGameNum()
    {
        if (playerData != null && playerData.gameNum <= 1)
        {
            playerData.gameNum++;
            SetGameNun(playerData.gameNum);
            SavePlayerData();
        }
    }
    public void SetGameNun(int number) => playerData.gameNum = number;
    public void SetTime(int time) => playerData.time = time;
    public void SetCoins(int coins) => playerData.coins = coins;
    public void SetGems(int gems) => playerData.gems = gems;
    public void SetShipInUseID(int id) => playerData.shipInUseId = id;
    public void AddAdquiredShipToData(int id) => playerData.adquiredShipsID.Add(id);
    #endregion

    #region Settings Data
    [ContextMenu("SaveSettingsData")]
    public void SaveSettingsData() => DataSerializer.SaveSettings(settingsData);
    public void LoadSettingsData() => settingsData = DataSerializer.LoadSettings();
    public void ApplySettingsByData()
    {
        Screen.brightness = GetSettingsData().brightness;
        Application.targetFrameRate = GetSettingsData().frameRate;
        QualitySettings.SetQualityLevel(GetSettingsData().quality);
        QualitySettings.antiAliasing = GetSettingsData().antiAliasing ? 2 : 0;
    }
    public SettingsData GetSettingsData() => settingsData;

    // Controls
    public void SetGyroInUse(bool value) => settingsData.gyroInUse = value;
    public void SetSesitivity(float sensitivity) => settingsData.sensitivity = sensitivity;

    // Screen
    public void SetFrameRate(int frameRateIndex) => settingsData.frameRate = frameRateIndex;
    public void SetCameraFOV(float cameraFOV) => settingsData.cameraFOV = cameraFOV;
    public void SetBrightness(float brightness) => settingsData.brightness = brightness;
    public void SetQuality(int quialityIndex) => settingsData.quality = quialityIndex;
    public void SetAntiAliasing(bool value) => settingsData.antiAliasing = value;

    // Sound
    public void SetGeneralVolume(float generalVolume) => settingsData.generalVolume = generalVolume;
    public void SetMusicVolume(float musicVolume) => settingsData.musicVolume = musicVolume;
    public void SetEffectsVolume(float effectsVolume) => settingsData.effectsVolume = effectsVolume;
    #endregion

    public void ExitGame()
    {
        SavePlayerData();
        SaveSettingsData();
        Application.Quit();
    }
}
