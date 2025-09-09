using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[Serializable]
public class SettingsWindows
{
    public GameObject window;
    public Button button;

    public SettingsWindows (GameObject window, Button button)
    {
        this.window = window;
        this.button = button;
    }
}

public class SettingsMenuController : MonoBehaviour
{
    [Header("Change Window Control")]
    [SerializeField] private List<SettingsWindows> windows;

    void Start()
    {
        SetupMenuButtons();
    }

    private void SetupMenuButtons()
    {
        foreach (SettingsWindows currentMenu in windows)
        {
            if (currentMenu.button == null) continue;

            GameObject indicator;

            currentMenu.button.onClick.AddListener(() =>
            {
                // Desactivar todas las ventanas
                foreach (SettingsWindows window in windows)
                {
                    indicator = window.button.transform.parent.GetChild(0).gameObject;
                    indicator.SetActive(false);
                    window.window.SetActive(false);
                }

                indicator = currentMenu.button.transform.parent.GetChild(0).gameObject;
                indicator.SetActive(true);
                currentMenu.window.SetActive(true);
            });
        }
    }
}