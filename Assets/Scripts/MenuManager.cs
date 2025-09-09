using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public struct MenuStruct
{
    public GameObject menu;
    public Button enterButton;
    public Button exitButton;
}

public class MenuManager : MonoBehaviour
{
    [Header("Transicion")]
    public Image fadeImage;
    [SerializeField] private float imageFadeSpeed = 7f;
    [SerializeField] private float imageFadeDelayA = 18f;
    [SerializeField] private float imageFadeDelayB = 5f;

    [Space(10)]
    public TextMeshProUGUI fadeText;
    [SerializeField] private float textFadeSpeed = 7f;
    [SerializeField] private float textFadeInDelay = 2f;
    [SerializeField] private float textFadeOutDelay = 2f;

    [Header("Menu")]
    [SerializeField] private Animator mainMenuAnimator;
    [SerializeField] private GameObject firstSelected;

    [Header("Player Data")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemsText;

    [Header("Change Menu Control")]
    [SerializeField] private List<MenuStruct> menus;

    private LensDistortionManager m_DistortionEffect;
    private MusicManager m_MusicManager;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        m_MusicManager = FindAnyObjectByType<MusicManager>();
        m_DistortionEffect = GetComponent<LensDistortionManager>();
        m_DistortionEffect.ResetEvents();
        FadeManager.ResetEvents();
        SetupMenuButtons();
        StartMenu();
        SetDataToUI();
    }

    private GameObject FindMenu(string name)
    {
        foreach (var currentMenu in menus)
        {
            if (currentMenu.menu.name == name)
            {
                return currentMenu.menu;
            }
        }
        return null;
    }
    private void SetupMenuButtons()
    {
        var mainMenu = FindMenu("Main Menu");

        foreach (MenuStruct currentMenu in menus)
        {
            if (currentMenu.enterButton == null || currentMenu.exitButton == null) 
                continue;

            currentMenu.enterButton.onClick.AddListener(() =>
            {
                LensDistortionManager.OnEffectAwait += () =>
                {
                    // Desactivar todos los menús
                    foreach (MenuStruct menu in menus)
                        menu.menu.SetActive(false);

                    currentMenu.menu.SetActive(true);
                };

                m_DistortionEffect.StartCompleteEffect(0);
            });

            currentMenu.exitButton.onClick.AddListener(() =>
            {
                LensDistortionManager.OnEffectAwait += () =>
                {
                    currentMenu.menu.SetActive(false);
                    mainMenu.SetActive(true);
                    mainMenuAnimator.SetTrigger("Start");
                };

                m_DistortionEffect.StartCompleteEffect(0);
            });
        }
    }
    private void StartMenu() 
    {
        PlayerData gameData = GameManager.Instance.GetPlayerData();

        if (gameData.gameNum == 1)
        {
            StartCoroutine(FadeManager.FadeIN(fadeText, textFadeSpeed, textFadeInDelay));
            StartCoroutine(FadeManager.FadeOUT(fadeText, textFadeSpeed, textFadeOutDelay));
        }

        FadeManager.OnFinishFadeOut += () => mainMenuAnimator.SetTrigger("Start");
        float fadeDelay = gameData.gameNum == 1 ? imageFadeDelayA : imageFadeDelayB;
        StartCoroutine(FadeManager.FadeOUT(fadeImage, imageFadeSpeed, fadeDelay));
    }   
    private void SetDataToUI()
    {
        PlayerData gameData = GameManager.Instance.GetPlayerData();

        coinsText.text = gameData.coins.ToString();
        gemsText.text = gameData.gems.ToString();

        int timeInSeconds = GameManager.Instance.GetPlayerData().time;
        string formattedTime = FormatTime(timeInSeconds);
        timeText.text = formattedTime;
    }
    private string FormatTime(int timeInSeconds)
    {
        var hours = timeInSeconds / 3600;
        var minutes = (timeInSeconds % 3600) / 60;
        var seconds = timeInSeconds % 60;

        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }

    public void StartGame() // Called by UI button
    {
        m_MusicManager.Stop(1);
        LensDistortionManager.OnEffectAwait += () => LevelLoader.LoadLevel("Game");
        m_DistortionEffect.StartCompleteEffect(0);
        GameManager.Instance.IncreaseGameNum();
    }
}
