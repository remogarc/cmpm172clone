using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    
    [Header("Language Settings")]
    public string currentLanguage = "English";
    
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
            {"camera", "Camera"},
            {"audio", "Audio"},
            {"controls", "Controls"},
            {"back", "Back"},
            {"resume", "Resume"},
            {"exit", "Exit"},
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
            {"camera", "Cámara"},
            {"audio", "Audio"},
            {"controls", "Controles"},
            {"back", "Atrás"},
            {"resume", "Continuar"},
            {"exit", "Salir"},
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
            {"camera", "Caméra"},
            {"audio", "Audio"},
            {"controls", "Contrôles"},
            {"back", "Retour"},
            {"resume", "Reprendre"},
            {"exit", "Quitter"},
            {"save", "Sauvegarder"},
            {"load", "Charger"},
            {"x_sensitivity", "Sensibilité X"},
            {"y_sensitivity", "Sensibilité Y"},
            {"field_of_view", "Champ de Vision"},
            {"music_volume", "Volume Musique"},
            {"invert_x", "Inverser X"},
            {"invert_y", "Inverser Y"}
        };
    }
    
    public void ChangeLanguage(string newLanguage)
    {
        if (translations.ContainsKey(newLanguage))
        {
            currentLanguage = newLanguage;
            SaveLanguage();
            UpdateAllTexts();
            Debug.Log("Language changed to: " + newLanguage);
        }
        else
        {
            Debug.LogWarning("Language not supported: " + newLanguage);
        }
    }
    
    public string GetTranslation(string key)
    {
        if (translations.ContainsKey(currentLanguage) && translations[currentLanguage].ContainsKey(key))
        {
            return translations[currentLanguage][key];
        }
        
        // Fallback to English if current language doesn't have the key
        if (translations.ContainsKey("English") && translations["English"].ContainsKey(key))
        {
            return translations["English"][key];
        }
        
        // If all else fails, return the key itself
        Debug.LogWarning("Translation not found for key: " + key);
        return key;
    }
    
    public void RegisterLocalizedText(LocalizedText localizedText)
    {
        if (!localizedTexts.Contains(localizedText))
        {
            localizedTexts.Add(localizedText);
        }
    }
    
    public void UnregisterLocalizedText(LocalizedText localizedText)
    {
        if (localizedTexts.Contains(localizedText))
        {
            localizedTexts.Remove(localizedText);
        }
    }
    
    private void UpdateAllTexts()
    {
        foreach (LocalizedText localizedText in localizedTexts)
        {
            if (localizedText != null)
            {
                localizedText.UpdateText();
            }
        }
    }
    
    private void SaveLanguage()
    {
        PlayerPrefs.SetString("Language", currentLanguage);
        PlayerPrefs.Save();
    }
    
    private void LoadSavedLanguage()
    {
        currentLanguage = PlayerPrefs.GetString("Language", "English");
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
} 