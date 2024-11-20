using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizedTextComponent : MonoBehaviour
{
    [SerializeField] private string tableReference;
    [SerializeField] private string localizationKey;

    private LocalizedString localizedStr;
    private Text textComponent;
    void Start()
    {
        textComponent = GetComponent<Text>();
        localizedStr = new LocalizedString { TableReference = tableReference, TableEntryReference = localizationKey };

        //just for testing
        var frenchLocale = LocalizationSettings.AvailableLocales.GetLocale("fr");
        LocalizationSettings.SelectedLocale = frenchLocale;

        LocalizationSettings.SelectedLocaleChanged += UpdateText;
        UpdateText(LocalizationSettings.SelectedLocale);
    }
    void UpdateText(Locale locale)
    {
        textComponent.text = localizedStr.GetLocalizedString();
    }
}
