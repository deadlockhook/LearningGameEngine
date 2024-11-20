using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
public class LocalizedTextComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string tableReference; //name of the table [UI here in example]
    [SerializeField] private string localizationKey; //key 

    private LocalizedString localizedStr; // This holds the key and refernce , actual translation is done here
    private Text textComponent;    //Text comp we are going to localize

    void Awake()
    {
        textComponent = GetComponent<Text>();
        localizedStr = new LocalizedString { TableReference = tableReference, TableEntryReference = localizationKey };

        LocalizationSettings.SelectedLocaleChanged += UpdateText;

        var frenchLocale = LocalizationSettings.AvailableLocales.GetLocale("fr");
        LocalizationSettings.SelectedLocale = frenchLocale;

        UpdateText(frenchLocale);
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= UpdateText;
    }
 
    void UpdateText(Locale locale)
    {
        textComponent.text = localizedStr.GetLocalizedString(); // actual translation logic is executed here
    }
}
