using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TranslatorController : MonoBehaviour
{
	// PLEASE READ !!!
	// Before editing, please rename this file!
	// Else if i update this asset, all you changes will be overwritten!
	// PLEASE READ !!!

	// GameObject name, used for 'NeedsToContain'
	public string GameObjectTranslate;

	//Text GameObject's name needs to include "[+]" to translate.
	// Please edit the NeedsToContain here, if needed!
	public string NeedsToContain = "[+]";

	// Prefix for logging
	public string TranslatorPrefix = "[AutoTranslator]";

	public string SelectedLanguage;
    public string DefaultLanguage;

	public TextMeshProUGUI translateText;

    // All the languages, you can add or remove any of these!
    // Remember to edit these IN EDITOR!
    [TextArea(5, 5)] public string Language_EN = "English";
    [TextArea(5, 5)] public string Language_ES = "Spanish";

	// Start function for automatic translation

	public void Start()
	{
		// First we check íf DefaultLanguage is specified.
		// And if not, we specify it!
		if (DefaultLanguage == "")
		{
			DefaultLanguage = PlayerPrefs.GetString("LanguageInUse", "Spanish");
			Debug.Log (TranslatorPrefix + " Default Language set as " + DefaultLanguage);
		}

		// No we check if the GameObject needs to be translated.
		GameObjectTranslate = this.gameObject.name;

		// Checks if GameObject name includes 'NeedsToContain'
		if (GameObjectTranslate.Contains (NeedsToContain))
		{
			translateText = GetComponentInChildren<TextMeshProUGUI> ();

			// If you want to disable AutoTranslation, please enter two (2) slashes (//) infront of 'Translate(); in the next row.'
			Translate ();
		}
		else
		{
			// If no 'NeedsToContain' is found, print a log.
			Debug.Log (TranslatorPrefix + " " + GameObjectTranslate + " doesn't contain " + NeedsToContain);
			return;
		}
	}

	public void SelectLanguage(string Language)
	{
		//You can make your own LanguageSelection screen here!

		SelectedLanguage = Language;
	}

	public void Translate()
	{
		// If no language is selected, select the 'DefaultLanguage'
		if(SelectedLanguage == "")
		SelectedLanguage = DefaultLanguage;

        // Translate Text to Languages
        if (SelectedLanguage == "Spanish")
        {
            translateText.text = Language_ES;
			PlayerPrefs.SetString("LanguageInUse", "Spanish");
            return;
        }

		if (SelectedLanguage == "English")
		{
			translateText.text = Language_EN;
            PlayerPrefs.SetString("LanguageInUse", "English");
			return;
		}

		// If 'SelectedLanguage' is not found, call error.
		Debug.LogError ("Language not found in: " + this.gameObject.name);
	}
}