using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeManager : MonoBehaviour
{
    public QuestSystem questSystem;
    public QuestTracker questTrack;
    public GameObject MainMenu;
    public Light SunLight;
    public GameObject Meteorite;
    public GameObject lightImage;

    public void Wish(int quest_ID)
    {
        questSystem.FindQuestInDatabase(quest_ID).isClaimd = true;
        questTrack.AddToComplete(quest_ID);

        MainMenu.SetActive(false);
        SunLight.enabled = false;
        Meteorite.SetActive(true);
        lightImage.SetActive(false);
    }


}
