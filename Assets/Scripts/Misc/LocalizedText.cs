using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{
    [Header("Localization Settings")]
    [Tooltip("The key to look up in the translation dictionary")]
    public string translationKey;
    
    private Text textComponent;
    
    void Start()
    {
        textComponent = GetComponent<Text>();
        
        // Register this text component with the Language Manager
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.RegisterLocalizedText(this);
            UpdateText();
        }
        else
        {
            Debug.LogWarning("LanguageManager not found! Make sure there's a LanguageManager in the scene.");
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
        }
    }
    
    // Method to change the translation key at runtime if needed
    public void SetTranslationKey(string newKey)
    {
        translationKey = newKey;
        UpdateText();
    }
} 