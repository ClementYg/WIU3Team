using UnityEngine;
[CreateAssetMenu(fileName = "LoreEntry", menuName = "ScriptableObjects/Bestiary/LoreEntry")]

//Honestly i really made this js in case we hv lore only specific stuff LOL
public class LoreEntryData : ScriptableObject, BestiaryEntry
{
    [Header("ID")]
    public string entryID;
    [Delayed] public string displayName;
    public Sprite icon;

    [Header("Description")]
    public string description;

    public string EntryID => entryID; //EntryID from BestiaryCategory Interface
    public string DisplayName => displayName;
    public string Description => description;
    public Sprite Icon => icon;
    public BestiaryCategory Category => BestiaryCategory.Enemy;

#if UNITY_EDITOR
    private void OnValidate()
    {
        //Auto-set entryID using displayName and setting all - and space to underscores(_)
        if (string.IsNullOrEmpty(entryID) && !string.IsNullOrEmpty(displayName))
        {
            //Usually use ToLowerInvarient when its code-based and not for viewing
            //Main difference is that ToLower() respects language-specific conventions 
            //so its more suitable for reading output etc
            entryID = displayName.ToLowerInvariant().Replace("-", "_").Replace(" ", "_");
        }
    }
#endif
}