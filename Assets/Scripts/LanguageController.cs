using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    public Button englishButton;
    public Button spanishButton;
    public Button frenchButton;
    public Button chineseButton;

    private void Start()
    {
        if (englishButton != null)
            englishButton.onClick.AddListener(() => SetLanguage(LanguageManager.Language.English));
        if (spanishButton != null)
            spanishButton.onClick.AddListener(() => SetLanguage(LanguageManager.Language.Spanish));
        if (frenchButton != null)
            frenchButton.onClick.AddListener(() => SetLanguage(LanguageManager.Language.French));
        if (chineseButton != null)
            chineseButton.onClick.AddListener(() => SetLanguage(LanguageManager.Language.Chinese));
    }

    private void SetLanguage(LanguageManager.Language language)
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.SetLanguage(language);
        }
    }
} 