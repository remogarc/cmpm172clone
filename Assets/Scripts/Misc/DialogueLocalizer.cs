using UnityEngine;

[System.Serializable]
public class DialogueKeyMapping
{
    [Tooltip("A readable name for this mapping")]
    public string displayName;
    
    [Tooltip("The dialogue key to use for localization")]
    public string dialogueKey;
    
    [Tooltip("The GameObject with the InteractionHandler that should use this dialogue key")]
    public GameObject targetObject;
}

/// <summary>
/// Helper component to easily set up dialogue localization for multiple objects in a scene.
/// This is useful for level designers to quickly configure dialogue keys without having to 
/// manually add LocalizedDialogue components to every object.
/// </summary>
public class DialogueLocalizer : MonoBehaviour
{
    [Header("Dialogue Key Mappings")]
    [Tooltip("Map dialogue keys to game objects with InteractionHandler components")]
    public DialogueKeyMapping[] dialogueMappings;
    
    [Header("Settings")]
    [Tooltip("Automatically set up LocalizedDialogue components on Start")]
    public bool autoSetupOnStart = true;
    
    [Tooltip("Enable debug logging for this component")]
    public bool enableDebugLogs = true;
    
    void Start()
    {
        if (autoSetupOnStart)
        {
            SetupAllDialogueMappings();
        }
    }
    
    [ContextMenu("Setup All Dialogue Mappings")]
    public void SetupAllDialogueMappings()
    {
        if (dialogueMappings == null || dialogueMappings.Length == 0)
        {
            if (enableDebugLogs)
                Debug.LogWarning("No dialogue mappings configured in DialogueLocalizer");
            return;
        }
        
        int successCount = 0;
        int totalCount = dialogueMappings.Length;
        
        for (int i = 0; i < dialogueMappings.Length; i++)
        {
            DialogueKeyMapping mapping = dialogueMappings[i];
            
            if (mapping.targetObject == null)
            {
                Debug.LogWarning($"DialogueLocalizer: Target object is null for mapping '{mapping.displayName}' (index {i})");
                continue;
            }
            
            if (string.IsNullOrEmpty(mapping.dialogueKey))
            {
                Debug.LogWarning($"DialogueLocalizer: Dialogue key is empty for mapping '{mapping.displayName}' on object '{mapping.targetObject.name}'");
                continue;
            }
            
            if (SetupDialogueLocalization(mapping.targetObject, mapping.dialogueKey, mapping.displayName))
            {
                successCount++;
            }
        }
        
        if (enableDebugLogs)
        {
            Debug.Log($"<color=green>DialogueLocalizer: Successfully set up {successCount}/{totalCount} dialogue mappings</color>");
        }
    }
    
    /// <summary>
    /// Sets up dialogue localization for a specific GameObject
    /// </summary>
    /// <param name="targetObject">The GameObject with an InteractionHandler</param>
    /// <param name="dialogueKey">The dialogue key to use for localization</param>
    /// <param name="displayName">A readable name for logging purposes</param>
    /// <returns>True if setup was successful, false otherwise</returns>
    public bool SetupDialogueLocalization(GameObject targetObject, string dialogueKey, string displayName = "")
    {
        if (targetObject == null)
        {
            Debug.LogError("DialogueLocalizer: Target object is null");
            return false;
        }
        
        InteractionHandler interactionHandler = targetObject.GetComponent<InteractionHandler>();
        if (interactionHandler == null)
        {
            Debug.LogError($"DialogueLocalizer: No InteractionHandler found on object '{targetObject.name}' for mapping '{displayName}'");
            return false;
        }
        
        LocalizedDialogue existingLocalizedDialogue = targetObject.GetComponent<LocalizedDialogue>();
        if (existingLocalizedDialogue != null)
        {
            // Update existing component
            existingLocalizedDialogue.dialogueKey = dialogueKey;
            existingLocalizedDialogue.UpdateDialogue();
            
            if (enableDebugLogs)
                Debug.Log($"<color=cyan>DialogueLocalizer: Updated existing LocalizedDialogue on '{targetObject.name}' with key '{dialogueKey}'</color>");
        }
        else
        {
            // Add new component
            LocalizedDialogue localizedDialogue = targetObject.AddComponent<LocalizedDialogue>();
            localizedDialogue.dialogueKey = dialogueKey;
            localizedDialogue.enableDebugLogs = enableDebugLogs;
            
            if (enableDebugLogs)
                Debug.Log($"<color=green>DialogueLocalizer: Added LocalizedDialogue to '{targetObject.name}' with key '{dialogueKey}'</color>");
        }
        
        return true;
    }
    
    /// <summary>
    /// Adds a new dialogue mapping at runtime
    /// </summary>
    /// <param name="displayName">A readable name for this mapping</param>
    /// <param name="dialogueKey">The dialogue key to use</param>
    /// <param name="targetObject">The GameObject to apply the mapping to</param>
    public void AddDialogueMapping(string displayName, string dialogueKey, GameObject targetObject)
    {
        // Resize the array to accommodate the new mapping
        DialogueKeyMapping[] newMappings = new DialogueKeyMapping[dialogueMappings.Length + 1];
        for (int i = 0; i < dialogueMappings.Length; i++)
        {
            newMappings[i] = dialogueMappings[i];
        }
        
        // Add the new mapping
        newMappings[dialogueMappings.Length] = new DialogueKeyMapping
        {
            displayName = displayName,
            dialogueKey = dialogueKey,
            targetObject = targetObject
        };
        
        dialogueMappings = newMappings;
        
        // Immediately set up the new mapping
        SetupDialogueLocalization(targetObject, dialogueKey, displayName);
        
        if (enableDebugLogs)
            Debug.Log($"<color=green>DialogueLocalizer: Added new mapping '{displayName}' with key '{dialogueKey}' for object '{targetObject.name}'</color>");
    }
    
    /// <summary>
    /// Removes all LocalizedDialogue components from mapped objects
    /// </summary>
    [ContextMenu("Remove All Dialogue Localizations")]
    public void RemoveAllDialogueLocalizations()
    {
        if (dialogueMappings == null) return;
        
        int removedCount = 0;
        
        foreach (DialogueKeyMapping mapping in dialogueMappings)
        {
            if (mapping.targetObject != null)
            {
                LocalizedDialogue localizedDialogue = mapping.targetObject.GetComponent<LocalizedDialogue>();
                if (localizedDialogue != null)
                {
                    if (Application.isPlaying)
                    {
                        Destroy(localizedDialogue);
                    }
                    else
                    {
                        DestroyImmediate(localizedDialogue);
                    }
                    removedCount++;
                }
            }
        }
        
        if (enableDebugLogs)
            Debug.Log($"<color=orange>DialogueLocalizer: Removed {removedCount} LocalizedDialogue components</color>");
    }
    
    [ContextMenu("Debug: Print All Mappings")]
    public void DebugPrintAllMappings()
    {
        if (dialogueMappings == null || dialogueMappings.Length == 0)
        {
            Debug.Log("DialogueLocalizer: No dialogue mappings configured");
            return;
        }
        
        Debug.Log($"DialogueLocalizer: {dialogueMappings.Length} dialogue mappings:");
        for (int i = 0; i < dialogueMappings.Length; i++)
        {
            DialogueKeyMapping mapping = dialogueMappings[i];
            string objectName = mapping.targetObject != null ? mapping.targetObject.name : "NULL";
            Debug.Log($"  {i + 1}. '{mapping.displayName}' → Key: '{mapping.dialogueKey}' → Object: '{objectName}'");
        }
    }
} 