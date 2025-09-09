using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class QuestTracker : MonoBehaviour
{
    [Header("Data Bases")]
    public QuestSystem questSystem;
    public CelestialBodiesData celestialBodiesData;

    [Header("References of Function")]
    public PrizeManager prize;

    [Header("Lists of Quests")]
    public List<QuestClass> activeQuests = new List<QuestClass>();
    public List<QuestClass> completeQuests = new List<QuestClass>();

    [Header("References to UI")]
    public Button[] prizeButton;
    public TextMeshProUGUI[] textQuest;
    public TextMeshProUGUI[] textButton;
    public Image[] imageList;

    private Color c;

    void Update()
    {
        //Asignación del texto de los botones
        if (textButton != null)
        {
            //Asignación de descripciones de las misiones
            for (int i = 0; i < textButton.Length; i++)
            {
                if (questSystem.FindQuestInDatabase(i).isClaimd != false)
                {
                    textButton[i].text = "Claimd";
                    prizeButton[i].interactable = false;
                }
                else textButton[i].text = "Claim";
            }
        }

        //Agrega descripciones de las misiones
        if (textQuest != null)
        {
            //Asignación de descripciones de las misiones
            for (int i = 0; i < textQuest.Length; i++)
            {
                if (questSystem.FindQuestInDatabase(i).isComplete != false)
                {                  
                    c = Color.blue;
                    c.a = 0.35f;
                    imageList[i].color = c;

                    if (questSystem.FindQuestInDatabase(i).isClaimd != false)
                    {
                        prizeButton[i].interactable = false;
                    }
                    else
                    {
                        prizeButton[i].interactable = true;
                    }
                }
                else
                {                   
                    prizeButton[i].interactable = false;
                }
            }
        }
    }

    public void UpdateQuest(int quest_ID, int amountObjs)
    {
        var val = activeQuests.Find(x => x.id == quest_ID);

        if(amountObjs > 0)
        {
            if(amountObjs >= val.itemsToBeCollected[quest_ID].amountToCollected)
            {
                val.complete = true;
                
                if(val.complete == true)
                {
                    questSystem.FindQuestInDatabase(quest_ID).isComplete = true;
                }
            }
        }
    }

    public void AddToComplete(int index)
    {
        if(questSystem.FindQuestInDatabase(index).isComplete != false && questSystem.FindQuestInDatabase(index).isClaimd != false)
        {
            completeQuests.Add(new QuestClass
            {
                id = index,
                type = (QuestClass.QuestType)questSystem.FindQuestInDatabase(index).type
            });
        }
    }
}

