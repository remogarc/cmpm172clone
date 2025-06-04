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
    
    // Dictionary to store dialogue translations
    private Dictionary<string, Dictionary<string, DialogueTranslation>> dialogueTranslations;
    
    // List to keep track of all text components that need updating
    private List<LocalizedText> localizedTexts = new List<LocalizedText>();
    
    // List to keep track of all localized dialogue components
    private List<LocalizedDialogue> localizedDialogues = new List<LocalizedDialogue>();
    
    void Awake()
    {
        // Singleton pattern - only one LanguageManager can exist
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeTranslations();
            InitializeDialogueTranslations();
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
    
    void InitializeDialogueTranslations()
    {
        dialogueTranslations = new Dictionary<string, Dictionary<string, DialogueTranslation>>();
        
        // Initialize dialogue translations for each language
        dialogueTranslations["English"] = new Dictionary<string, DialogueTranslation>();
        dialogueTranslations["Spanish"] = new Dictionary<string, DialogueTranslation>();
        dialogueTranslations["French"] = new Dictionary<string, DialogueTranslation>();
        
        // Example dialogue entries - you can add more here
        
        // Jeff dialogue
        dialogueTranslations["English"]["jeff"] = new DialogueTranslation
        {
            name = "Jeff",
            sentences = new string[]
            {
                "Freakin' Frijoles! An Inkeeper! How is this even possible!? I thought they had gone extinct!",
                "Oh pardon my manners, my name is Jeff! I'm a resident here. My life got uprooted after the drowning, but I'm making ends meet!",
                "If you ever want to know more about this world, let me know!"
            }
        };
        
        dialogueTranslations["Spanish"]["jeff"] = new DialogueTranslation
        {
            name = "Jeff",
            sentences = new string[]
            {
                "¡Frijoles malditos! ¡Un Posadero! ¡¿Cómo es esto posible?! ¡Pensé que se habían extinguido!",
                "Oh, perdona mis modales, ¡mi nombre es Jeff! Soy residente aquí. Mi vida cambió después del ahogamiento, ¡pero estoy saliendo adelante!",
                "¡Si alguna vez quieres saber más sobre este mundo, házmelo saber!"
            }
        };
        
        dialogueTranslations["French"]["jeff"] = new DialogueTranslation
        {
            name = "Jeff",
            sentences = new string[]
            {
                "Sacrés haricots ! Un Aubergiste ! Comment est-ce possible ?! Je pensais qu'ils avaient disparu !",
                "Oh, pardonnez mes manières, je m'appelle Jeff ! Je suis résident ici. Ma vie a été bouleversée après la noyade, mais je m'en sors !",
                "Si vous voulez en savoir plus sur ce monde, faites-le moi savoir !"
            }
        };
        
        // Old Book dialogue
        dialogueTranslations["English"]["old_book"] = new DialogueTranslation
        {
            name = "Old Book",
            sentences = new string[]
            {
                "...",
                "It seems to be a diary.",
                "It reads \"Password: It is both a question and an answer. The question is 'Why did the....'\"",
                "You are unable to decipher the end of the sentence....",
                "The diary goes on.",
                "\"The king has ordered me to build this beacon. Decades of my service have finally paid off. I will see to it that this is the greatest piece of architecture an Inkeeper has ever made...\"",
                "You flip the pages to another entry",
                "\"Progress has been grim... Several of us have taken ill... We don't know what the illness is.. But we grow fewer in number with each passing moon...\"",
                "You flip the pages to another entry",
                "\"We have lost too many of our brethren...\"",
                "\"To my dismay it is my turn to part ways with this world as well....\" ",
                "\"The king must be warned... Someone must go to the capital...\"",
                "\"Before it's too late.\""
            }
        };
        
        dialogueTranslations["Spanish"]["old_book"] = new DialogueTranslation
        {
            name = "Libro Viejo",
            sentences = new string[]
            {
                "...",
                "Parece ser un diario.",
                "Dice \"Contraseña: Es tanto una pregunta como una respuesta. La pregunta es '¿Por qué...?'\"",
                "No puedes descifrar el final de la oración....",
                "El diario continúa.",
                "\"El rey me ha ordenado construir este faro. Décadas de mi servicio finalmente han dado fruto. Me aseguraré de que esta sea la mayor obra arquitectónica que un Posadero haya hecho jamás...\"",
                "Pasas las páginas a otra entrada",
                "\"El progreso ha sido sombrío... Varios de nosotros han enfermado... No sabemos qué es la enfermedad... Pero somos menos con cada luna que pasa...\"",
                "Pasas las páginas a otra entrada",
                "\"Hemos perdido demasiados de nuestros hermanos...\"",
                "\"Para mi pesar, es mi turno de partir de este mundo también....\"",
                "\"El rey debe ser advertido... Alguien debe ir a la capital...\"",
                "\"Antes de que sea demasiado tarde.\""
            }
        };
        
        dialogueTranslations["French"]["old_book"] = new DialogueTranslation
        {
            name = "Vieux Livre",
            sentences = new string[]
            {
                "...",
                "Il semble être un journal.",
                "Il dit \"Mot de passe : C'est à la fois une question et une réponse. La question est 'Pourquoi...?'\"",
                "Vous ne pouvez pas déchiffrer la fin de la phrase....",
                "Le journal continue.",
                "\"Le roi m'a ordonné de construire ce phare. Des décennies de mon service ont finalement porté leurs fruits. Je veillerai à ce que ce soit la plus grande œuvre architecturale qu'un Aubergiste ait jamais réalisée...\"",
                "Vous tournez les pages vers une autre entrée",
                "\"Les progrès ont été sombres... Plusieurs d'entre nous sont tombés malades... Nous ne savons pas ce qu'est la maladie... Mais nous diminuons en nombre à chaque lune qui passe...\"",
                "Vous tournez les pages vers une autre entrée",
                "\"Nous avons perdu trop de nos frères...\"",
                "\"À mon grand regret, c'est mon tour de quitter ce monde aussi....\"",
                "\"Le roi doit être prévenu... Quelqu'un doit aller à la capitale...\"",
                "\"Avant qu'il ne soit trop tard.\""
            }
        };
        
        if (enableDebugLogs)
        {
            Debug.Log($"LanguageManager: Loaded dialogue translations for {dialogueTranslations.Count} languages");
            foreach (var lang in dialogueTranslations.Keys)
            {
                Debug.Log($"  - {lang}: {dialogueTranslations[lang].Count} dialogue entries");
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
            UpdateAllDialogues();
            
            if (enableDebugLogs)
            {
                Debug.Log($"<color=green>Language changed from {oldLanguage} to {newLanguage}</color>");
                Debug.Log($"<color=cyan>Updated {localizedTexts.Count} text components and {localizedDialogues.Count} dialogue components</color>");
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
    
    public DialogueTranslation GetDialogueTranslation(string dialogueKey)
    {
        if (dialogueTranslations.ContainsKey(currentLanguage) && dialogueTranslations[currentLanguage].ContainsKey(dialogueKey))
        {
            DialogueTranslation translation = dialogueTranslations[currentLanguage][dialogueKey];
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"Dialogue translation '{dialogueKey}' → '{translation.name}' with {translation.sentences.Length} sentences ({currentLanguage})");
            return translation;
        }
        
        // Fallback to English if current language doesn't have the dialogue key
        if (dialogueTranslations.ContainsKey("English") && dialogueTranslations["English"].ContainsKey(dialogueKey))
        {
            DialogueTranslation fallback = dialogueTranslations["English"][dialogueKey];
            if (enableDebugLogs)
                Debug.LogWarning($"Using English fallback for dialogue '{dialogueKey}': '{fallback.name}'");
            return fallback;
        }
        
        // If all else fails, return null
        Debug.LogWarning($"Dialogue translation not found for key: '{dialogueKey}'");
        return null;
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
    
    public void RegisterLocalizedDialogue(LocalizedDialogue localizedDialogue)
    {
        if (!localizedDialogues.Contains(localizedDialogue))
        {
            localizedDialogues.Add(localizedDialogue);
            
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"<color=yellow>Registered LocalizedDialogue: {localizedDialogue.name} (key: '{localizedDialogue.dialogueKey}')</color>");
        }
    }
    
    public void UnregisterLocalizedDialogue(LocalizedDialogue localizedDialogue)
    {
        if (localizedDialogues.Contains(localizedDialogue))
        {
            localizedDialogues.Remove(localizedDialogue);
            
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"<color=orange>Unregistered LocalizedDialogue: {localizedDialogue.name}</color>");
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
    
    private void UpdateAllDialogues()
    {
        int updatedCount = 0;
        foreach (LocalizedDialogue localizedDialogue in localizedDialogues)
        {
            if (localizedDialogue != null)
            {
                localizedDialogue.UpdateDialogue();
                updatedCount++;
            }
        }
        
        if (enableDebugLogs)
            Debug.Log($"<color=cyan>Updated {updatedCount} dialogue components to {currentLanguage}</color>");
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
    
    [ContextMenu("Debug: Print Registered Dialogues")]
    public void DebugPrintRegisteredDialogues()
    {
        Debug.Log($"Registered LocalizedDialogue components: {localizedDialogues.Count}");
        for (int i = 0; i < localizedDialogues.Count; i++)
        {
            if (localizedDialogues[i] != null)
                Debug.Log($"  {i + 1}. {localizedDialogues[i].name} - Key: '{localizedDialogues[i].dialogueKey}'");
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

[System.Serializable]
public class DialogueTranslation
{
    public string name;
    public string[] sentences;
    public string[] quest_check;
    public string[] quest_complete;
    public string[] choices;
    
    public DialogueTranslation()
    {
        quest_check = new string[0];
        quest_complete = new string[0];
        choices = new string[0];
    }
} 