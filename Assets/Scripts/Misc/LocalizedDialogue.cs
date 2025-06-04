using UnityEngine;

public class LocalizedDialogue : MonoBehaviour
{
    [Header("Dialogue Localization Settings")]
    [Tooltip("The key to look up in the dialogue translation dictionary")]
    public string dialogueKey;
    
    [Header("Debug Settings")]
    public bool enableDebugLogs = false;
    
    private InteractionHandler interactionHandler;
    private DialogueTranslation lastDialogueTranslation;
    
    void Start()
    {
        interactionHandler = GetComponent<InteractionHandler>();
        
        if (interactionHandler == null)
        {
            Debug.LogError($"LocalizedDialogue requires an InteractionHandler component on the same GameObject! GameObject: {name}");
            return;
        }
        
        // Register this dialogue component with the Language Manager
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.RegisterLocalizedDialogue(this);
            UpdateDialogue();
            
            if (enableDebugLogs)
                Debug.Log($"<color=yellow>LocalizedDialogue '{name}' registered with key: '{dialogueKey}'</color>");
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
            LanguageManager.Instance.UnregisterLocalizedDialogue(this);
        }
    }
    
    public void UpdateDialogue()
    {
        if (interactionHandler != null && !string.IsNullOrEmpty(dialogueKey))
        {
            DialogueTranslation dialogueTranslation = LanguageManager.Instance.GetDialogueTranslation(dialogueKey);
            
            if (dialogueTranslation != null)
            {
                // Update the dialogue in the InteractionHandler
                interactionHandler.SetLocalizedDialogue(dialogueTranslation);
                
                if (enableDebugLogs && (lastDialogueTranslation == null || 
                    lastDialogueTranslation.name != dialogueTranslation.name))
                {
                    Debug.Log($"<color=cyan>Updated dialogue '{name}': '{lastDialogueTranslation?.name}' → '{dialogueTranslation.name}'</color>");
                    lastDialogueTranslation = dialogueTranslation;
                }
            }
            else
            {
                Debug.LogWarning($"Dialogue translation not found for key: '{dialogueKey}' on GameObject: {name}");
            }
        }
        else if (enableDebugLogs)
        {
            if (interactionHandler == null)
                Debug.LogWarning($"InteractionHandler component missing on {name}");
            if (string.IsNullOrEmpty(dialogueKey))
                Debug.LogWarning($"Dialogue key is empty on {name}");
        }
    }
    
    // Method to change the dialogue key at runtime if needed
    public void SetDialogueKey(string newKey)
    {
        if (enableDebugLogs)
            Debug.Log($"<color=magenta>Changed dialogue key on '{name}': '{dialogueKey}' → '{newKey}'</color>");
            
        dialogueKey = newKey;
        UpdateDialogue();
    }
    
    // Debug method to test this specific dialogue component
    [ContextMenu("Debug: Print Current Dialogue Translation")]
    public void DebugPrintCurrentDialogueTranslation()
    {
        if (LanguageManager.Instance != null)
        {
            DialogueTranslation translation = LanguageManager.Instance.GetDialogueTranslation(dialogueKey);
            if (translation != null)
            {
                Debug.Log($"Dialogue translation for '{name}' (key: '{dialogueKey}'): '{translation.name}' with {translation.sentences.Length} sentences");
                for (int i = 0; i < translation.sentences.Length; i++)
                {
                    Debug.Log($"  Sentence {i + 1}: {translation.sentences[i]}");
                }
            }
            else
            {
                Debug.LogWarning($"No dialogue translation found for key: '{dialogueKey}'");
            }
        }
    }
    
    [ContextMenu("Debug: Force Update")]
    public void DebugForceUpdate()
    {
        Debug.Log($"Force updating dialogue '{name}' with key '{dialogueKey}'");
        UpdateDialogue();
    }
} 