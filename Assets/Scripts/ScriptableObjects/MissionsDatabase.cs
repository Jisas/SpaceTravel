using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Mission System / Missions Database")]

public class MissionsDatabase : PersistentScriptableObject
{
    public List<Missions> missionsList = new List<Missions>();

    public Missions FindMissionInDatabase(int id)
    {
        foreach (Missions missions in missionsList)
        {
            if (missions.id == id)
            {
                return missions;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Missions
{
    public int id;
    [TextArea(5, 5)] public string description;
    [TextArea(5, 5)] public string englishDescription;
}