using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }

    public enum Language
    {
        English,
        Spanish,
        French,
        Chinese
    }

    [System.Serializable]
    public class LocalizedText
    {
        public string key;
        public string english;
        public string spanish;
        public string french;
        public string chinese;
    }

    public List<LocalizedText> localizedTexts = new List<LocalizedText>();
    public Language currentLanguage = Language.English;

    private Dictionary<string, LocalizedText> textDictionary = new Dictionary<string, LocalizedText>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize dictionary
        foreach (var text in localizedTexts)
        {
            textDictionary[text.key] = text;
        }
    }

    public void SetLanguage(Language language)
    {
        currentLanguage = language;
        UpdateAllTexts();
    }

    public string GetLocalizedText(string key)
    {
        if (textDictionary.TryGetValue(key, out LocalizedText text))
        {
            switch (currentLanguage)
            {
                case Language.English:
                    return text.english;
                case Language.Spanish:
                    return text.spanish;
                case Language.French:
                    return text.french;
                case Language.Chinese:
                    return text.chinese;
                default:
                    return text.english;
            }
        }
        return key; // Return the key if translation not found
    }

    private void UpdateAllTexts()
    {
        // Find all Text components
        Text[] texts = FindObjectsOfType<Text>();
        foreach (Text text in texts)
        {
            LocalizedTextComponent localizedText = text.GetComponent<LocalizedTextComponent>();
            if (localizedText != null)
            {
                text.text = GetLocalizedText(localizedText.key);
            }
        }

        // Find all TextMeshPro components
        TextMeshProUGUI[] tmpTexts = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in tmpTexts)
        {
            LocalizedTextComponent localizedText = text.GetComponent<LocalizedTextComponent>();
            if (localizedText != null)
            {
                text.text = GetLocalizedText(localizedText.key);
            }
        }
    }
} 