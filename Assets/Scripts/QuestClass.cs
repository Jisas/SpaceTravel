using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestClass
{
    public int id;
    public bool complete = false;
    public QuestType type;

    [Header("For Objects")]
    public List<Quest.ItemsToBeCollected> itemsToBeCollected = new List<Quest.ItemsToBeCollected>();

    public enum QuestType
    {
        HarvestingAndUses,
        TimeRecord,
        Piloting
    }
}
