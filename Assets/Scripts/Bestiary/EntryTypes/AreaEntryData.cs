using UnityEngine;
[CreateAssetMenu(fileName = "AreaEntry", menuName = "ScriptableObjects/Bestiary/AreaEntry")]

public class AreaEntryData : ScriptableObject, BestiaryEntry
{
    [Header("ID")]
    public string entryID;
    [Delayed] public string displayName;
    public Sprite icon;

    [Header("Description")]
    public string description;
    [Header("Map")]
    //the sprite needs a png that only shows this area's portion highlighted. (all will compile)
    //into a gigantic layered map afterwards.
    public Sprite regionSprite;
    public Vector2 mapPosition;
    public string EntryID => entryID; //EntryID from BestiaryCategory Interface
    public string DisplayName => displayName;
    public string Description => description;
    public Sprite Icon => icon;
    public BestiaryCategory Category => BestiaryCategory.Area;

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