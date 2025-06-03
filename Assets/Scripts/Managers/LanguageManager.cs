using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    
    [Header("Language Settings")]
    public string currentLanguage = "English";
    
    [Header("Debug Settings")]
    public bool enableDebugLogs = true;
    public bool showRegisteredTexts = true;
    
    // Dictionary to store all translations
    private Dictionary<string, Dictionary<string, string>> translations;
    
    // List to keep track of all text components that need updating
    private List<LocalizedText> localizedTexts = new List<LocalizedText>();
    
    void Awake()
    {
        // Singleton pattern - only one LanguageManager can exist
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeTranslations();
            LoadSavedLanguage();
            
            if (enableDebugLogs)
                Debug.Log($"LanguageManager initialized with language: {currentLanguage}");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void InitializeTranslations()
    {
        translations = new Dictionary<string, Dictionary<string, string>>();
        
        // English translations
        translations["English"] = new Dictionary<string, string>
        {
            {"options", "Options"},
            {"language", "Language"},
            {"spanish", "Spanish"},
            {"french", "French"},
            {"english", "English"},
            {"camera", "Camera"},
            {"audio", "Audio"},
            {"controls", "Controls"},
            {"back", "Back"},
            {"resume", "Resume"},
            {"start", "Start"},
            {"exit", "Quit"},
            {"quit", "Quit"},
            {"save", "Save"},
            {"load", "Load"},
            {"x_sensitivity", "X Sensitivity"},
            {"y_sensitivity", "Y Sensitivity"},
            {"field_of_view", "Field of View"},
            {"music_volume", "Music Volume"},
            {"invert_x", "Invert X"},
            {"invert_y", "Invert Y"}
        };
        
        // Spanish translations
        translations["Spanish"] = new Dictionary<string, string>
        {
            {"options", "Opciones"},
            {"language", "Idioma"},
            {"spanish", "Español"},
            {"french", "Francés"},
            {"english", "Inglés"},
            {"camera", "Cámara"},
            {"audio", "Audio"},
            {"controls", "Controles"},
            {"back", "Atrás"},
            {"resume", "Continuar"},
            {"start", "Empezar"},
            {"exit", "Salir"},
            {"quit", "Salir"},
            {"save", "Guardar"},
            {"load", "Cargar"},
            {"x_sensitivity", "Sensibilidad X"},
            {"y_sensitivity", "Sensibilidad Y"},
            {"field_of_view", "Campo de Visión"},
            {"music_volume", "Volumen de Música"},
            {"invert_x", "Invertir X"},
            {"invert_y", "Invertir Y"}
        };
        
        // French translations
        translations["French"] = new Dictionary<string, string>
        {
            {"options", "Options"},
            {"language", "Langue"},
            {"spanish", "Espagnol"},
            {"french", "Français"},
            {"english", "Anglais"},
            {"camera", "Caméra"},
            {"audio", "Audio"},
            {"controls", "Contrôles"},
            {"back", "Retour"},
            {"resume", "Reprendre"},
            {"start", "Commencer"},
            {"exit", "Quitter"},
            {"quit", "Quitter"},
            {"save", "Sauvegarder"},
            {"load", "Charger"},
            {"x_sensitivity", "Sensibilité X"},
            {"y_sensitivity", "Sensibilité Y"},
            {"field_of_view", "Champ de Vision"},
            {"music_volume", "Volume Musique"},
            {"invert_x", "Inverser X"},
            {"invert_y", "Inverser Y"}
        };
        
        if (enableDebugLogs)
        {
            Debug.Log($"LanguageManager: Loaded {translations.Count} languages");
            foreach (var lang in translations.Keys)
            {
                Debug.Log($"  - {lang}: {translations[lang].Count} translations");
            }
        }
    }
    
    public void ChangeLanguage(string newLanguage)
    {
        if (translations.ContainsKey(newLanguage))
        {
            string oldLanguage = currentLanguage;
            currentLanguage = newLanguage;
            SaveLanguage();
            UpdateAllTexts();
            
            if (enableDebugLogs)
            {
                Debug.Log($"<color=green>Language changed from {oldLanguage} to {newLanguage}</color>");
                Debug.Log($"<color=cyan>Updated {localizedTexts.Count} text components</color>");
            }
        }
        else
        {
            Debug.LogWarning($"Language not supported: {newLanguage}");
        }
    }
    
    public string GetTranslation(string key)
    {
        if (translations.ContainsKey(currentLanguage) && translations[currentLanguage].ContainsKey(key))
        {
            string translation = translations[currentLanguage][key];
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"Translation '{key}' → '{translation}' ({currentLanguage})");
            return translation;
        }
        
        // Fallback to English if current language doesn't have the key
        if (translations.ContainsKey("English") && translations["English"].ContainsKey(key))
        {
            string fallback = translations["English"][key];
            if (enableDebugLogs)
                Debug.LogWarning($"Using English fallback for '{key}': '{fallback}'");
            return fallback;
        }
        
        // If all else fails, return the key itself
        Debug.LogWarning($"Translation not found for key: '{key}' - returning key as fallback");
        return key;
    }
    
    public void RegisterLocalizedText(LocalizedText localizedText)
    {
        if (!localizedTexts.Contains(localizedText))
        {
            localizedTexts.Add(localizedText);
            
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"<color=yellow>Registered LocalizedText: {localizedText.name} (key: '{localizedText.translationKey}')</color>");
        }
    }
    
    public void UnregisterLocalizedText(LocalizedText localizedText)
    {
        if (localizedTexts.Contains(localizedText))
        {
            localizedTexts.Remove(localizedText);
            
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"<color=orange>Unregistered LocalizedText: {localizedText.name}</color>");
        }
    }
    
    private void UpdateAllTexts()
    {
        int updatedCount = 0;
        foreach (LocalizedText localizedText in localizedTexts)
        {
            if (localizedText != null)
            {
                localizedText.UpdateText();
                updatedCount++;
            }
        }
        
        if (enableDebugLogs)
            Debug.Log($"<color=cyan>Updated {updatedCount} text components to {currentLanguage}</color>");
    }
    
    private void SaveLanguage()
    {
        PlayerPrefs.SetString("Language", currentLanguage);
        PlayerPrefs.Save();
        
        if (enableDebugLogs)
            Debug.Log($"Saved language preference: {currentLanguage}");
    }
    
    private void LoadSavedLanguage()
    {
        string savedLanguage = PlayerPrefs.GetString("Language", "English");
        currentLanguage = savedLanguage;
        
        if (enableDebugLogs)
            Debug.Log($"Loaded saved language: {currentLanguage}");
    }
    
    // Methods for UI buttons to call
    public void SetLanguageToEnglish()
    {
        ChangeLanguage("English");
    }
    
    public void SetLanguageToSpanish()
    {
        ChangeLanguage("Spanish");
    }
    
    public void SetLanguageToFrench()
    {
        ChangeLanguage("French");
    }
    
    // Debug methods you can call from inspector or console
    [ContextMenu("Debug: Print Current Language")]
    public void DebugPrintCurrentLanguage()
    {
        Debug.Log($"Current Language: {currentLanguage}");
    }
    
    [ContextMenu("Debug: Print Registered Texts")]
    public void DebugPrintRegisteredTexts()
    {
        Debug.Log($"Registered LocalizedText components: {localizedTexts.Count}");
        for (int i = 0; i < localizedTexts.Count; i++)
        {
            if (localizedTexts[i] != null)
                Debug.Log($"  {i + 1}. {localizedTexts[i].name} - Key: '{localizedTexts[i].translationKey}'");
        }
    }
    
    [ContextMenu("Debug: Test All Languages")]
    public void DebugTestAllLanguages()
    {
        StartCoroutine(TestAllLanguagesCoroutine());
    }
    
    private IEnumerator TestAllLanguagesCoroutine()
    {
        string originalLanguage = currentLanguage;
        
        foreach (string language in translations.Keys)
        {
            Debug.Log($"--- Testing {language} ---");
            ChangeLanguage(language);
            yield return new WaitForSeconds(1f);
        }
        
        // Restore original language
        ChangeLanguage(originalLanguage);
    }
} 