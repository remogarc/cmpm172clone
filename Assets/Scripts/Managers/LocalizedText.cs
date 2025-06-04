using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{
    [Header("Localization Settings")]
    [Tooltip("The key to look up in the translation dictionary")]
    public string translationKey;
    
    [Header("Debug Settings")]
    public bool enableDebugLogs = false;
    
    private Text textComponent;
    private string lastTranslation = "";
    
    void Start()
    {
        textComponent = GetComponent<Text>();
        
        // Register this text component with the Language Manager
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.RegisterLocalizedText(this);
            UpdateText();
            
            if (enableDebugLogs)
                Debug.Log($"<color=yellow>LocalizedText '{name}' registered with key: '{translationKey}'</color>");
        }
        else
        {
            Debug.LogWarning($"LanguageManager not found! Make sure there's a LanguageManager in the scene. GameObject: {name}");
        }
    }
    
    void OnDestroy()
    {
        // Unregister when destroyed to prevent memory leaks
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.UnregisterLocalizedText(this);
        }
    }
    
    public void UpdateText()
    {
        if (textComponent != null && !string.IsNullOrEmpty(translationKey))
        {
            string translatedText = LanguageManager.Instance.GetTranslation(translationKey);
            textComponent.text = translatedText;
            
            if (enableDebugLogs && translatedText != lastTranslation)
            {
                Debug.Log($"<color=cyan>Updated '{name}': '{lastTranslation}' → '{translatedText}'</color>");
                lastTranslation = translatedText;
            }
        }
        else if (enableDebugLogs)
        {
            if (textComponent == null)
                Debug.LogWarning($"Text component missing on {name}");
            if (string.IsNullOrEmpty(translationKey))
                Debug.LogWarning($"Translation key is empty on {name}");
        }
    }
    
    // Method to change the translation key at runtime if needed
    public void SetTranslationKey(string newKey)
    {
        if (enableDebugLogs)
            Debug.Log($"<color=magenta>Changed translation key on '{name}': '{translationKey}' → '{newKey}'</color>");
            
        translationKey = newKey;
        UpdateText();
    }
    
    // Debug method to test this specific text component
    [ContextMenu("Debug: Print Current Translation")]
    public void DebugPrintCurrentTranslation()
    {
        if (LanguageManager.Instance != null)
        {
            string translation = LanguageManager.Instance.GetTranslation(translationKey);
            Debug.Log($"Translation for '{name}' (key: '{translationKey}'): '{translation}'");
        }
    }
    
    [ContextMenu("Debug: Force Update")]
    public void DebugForceUpdate()
    {
        Debug.Log($"Force updating '{name}' with key '{translationKey}'");
        UpdateText();
    }
} 