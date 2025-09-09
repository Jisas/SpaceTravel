using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [Header("Requiered Values")]
    public int id_Mision;
    public int currentAmount;

    [Header("References to Function")]
    public ShipsDatabase database;
    public CelestialBodiesData CBDB;
    public QuestSystem questDB;
    public QuestTracker questTrack;
    public QuestClass quest;

    public void Start()
    {
        Enumeration();
    }

    public void Enumeration()
    {
        for (int i = 0; i < questTrack.imageList.Length; i++)
        {
            GameObject[] obj;
            obj = GameObject.FindGameObjectsWithTag("ImgPanelQuest");
            obj[i].GetComponent<QuestGiver>().id_Mision = i;
        }

        ActiveAllQuest();
    }

    public void ActiveAllQuest()
    {
        questTrack.activeQuests.Add(new QuestClass 
        {
            id = id_Mision, 
            type = quest.type, 
            itemsToBeCollected = quest.itemsToBeCollected, 
        });

        Equivalece();
    } 

    public void Equivalece()
    {
        for (int i = 0; i < questDB.missions.Length; i++)
        {
            if(questDB.missions[i].id == this.id_Mision)
            {
                quest.id = questDB.missions[i].id;
                quest.type = (QuestClass.QuestType)questDB.missions[i].type;

                quest.itemsToBeCollected.Add(questDB.missions[i].objectsToBeCollected);
            }
        }

        UpdateMissions();
    }

    public void UpdateMissions()
    {
        if(quest.type == QuestClass.QuestType.HarvestingAndUses)
        {
            currentAmount = CBDB.FindObjectInDatabase(questDB.FindQuestInDatabase(id_Mision).objectsToBeCollected.objectID).objAmount;
            questTrack.UpdateQuest(id_Mision, currentAmount);
            return;
        }

        if(quest.type == QuestClass.QuestType.TimeRecord)
        {
            questTrack.UpdateQuest(id_Mision, PlayerPrefs.GetInt("TiempoTranscurrido"));
            return;
        }

        if(quest.type == QuestClass.QuestType.Piloting)
        {
            //int shipID = database.FindShipInUse().Index;
            //float skillShip = database.FindShipInDatabase(shipID).lvlSkill;
            //questTrack.UpdateQuest(id_Mision, (int)skillShip);
            return;
        }
    }
}
