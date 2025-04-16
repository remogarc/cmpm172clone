using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Text), typeof(TextMeshProUGUI))]
public class LocalizedTextComponent : MonoBehaviour
{
    public string key;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (LanguageManager.Instance != null)
        {
            string localizedText = LanguageManager.Instance.GetLocalizedText(key);
            
            // Try to update Text component
            Text text = GetComponent<Text>();
            if (text != null)
            {
                text.text = localizedText;
            }

            // Try to update TextMeshPro component
            TextMeshProUGUI tmpText = GetComponent<TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.text = localizedText;
            }
        }
    }
} 