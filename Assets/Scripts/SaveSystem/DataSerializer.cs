using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public static class DataSerializer
{
    public static void SavePlayer(PlayerData playerData)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/game.data";
        FileStream stream = new(path, FileMode.Create);

        PlayerData data = playerData;

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/game.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            PlayerData data = new()
            {
                time = 0,
                coins = 0,
                gems = 0,
                shipInUseId = 0,
                adquiredShipsID = new() { 0 }
            };

            return data;
        }
    }

    public static void SaveSettings(SettingsData settingsData)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/settings.data";
        FileStream stream = new(path, FileMode.Create);

        SettingsData data = settingsData;

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SettingsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            stream.Close();

            return data;
        }
        else
        {
            SettingsData data = new()
            {
                gyroInUse = false,
                sensitivity = 0.4f,
                frameRate = 30,
                cameraFOV = 100,
                brightness = Screen.brightness,
                quality = 1,
                antiAliasing = true,
                generalVolume = 1,
                musicVolume = 0.5f,
                effectsVolume = 0.7f
            };

            return data;
        }
    }
}
