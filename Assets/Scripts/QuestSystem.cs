using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System / Quest Database")]
public class QuestSystem : ScriptableObject
{   
    public Quest[] missions;

    public Quest FindQuestInDatabase(int id)
    {
        foreach (Quest mission in missions)
        {
            if (mission.id == id)
            {
                return mission;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Quest
{
    public int id;
    public bool isComplete;
    public bool isClaimd;
    public string spanishDescription;
    public string englishDescription;
    public QuestType type;

    [Header("Hearving and Uses Data")]
    public ItemsToBeCollected objectsToBeCollected;

    [System.Serializable]
    public enum QuestType
    {
        HarvestingAndUses,
        TimeRecord,
        Piloting
    }

    [System.Serializable]
    public struct ItemsToBeCollected
    {
        public int amountToCollected;
        public int objectID;
    }
}
