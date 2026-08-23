using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif 

[CreateAssetMenu(fileName = "BestiaryDatabase", menuName = "ScriptableObjects/Bestiary/BestiaryDatabase")]
public class BestiaryDatabase : ScriptableObject
{
    public List<ItemData> itemEntries = new();
    public List<AreaEntryData> areaEntries = new();
    public List<EnemyEntryData> enemyEntries = new();
    public List<LoreEntryData> loreEntries = new();

    public List<BestiaryEntry> GetAllEntries()
    {
        List<BestiaryEntry> all = new();
        all.AddRange(itemEntries);
        all.AddRange(areaEntries);
        all.AddRange(enemyEntries);
        all.AddRange(loreEntries);
        return all;
    }

    public List<BestiaryEntry> GetByCategory(BestiaryCategory category)
    {
        return GetAllEntries().FindAll(e => e.Category == category); 
    }

#if UNITY_EDITOR
    [ContextMenu("Find All Entries In Project")]
    private void FindAllEntries()
    {
        itemEntries = FindAssetsOfType<ItemData>();
        areaEntries = FindAssetsOfType<AreaEntryData>();
        enemyEntries = FindAssetsOfType<EnemyEntryData>();
        loreEntries = FindAssetsOfType<LoreEntryData>();

        EditorUtility.SetDirty(this);
        Debug.Log($"BestiaryDatabase: found {itemEntries.Count} items, {areaEntries.Count} areas, " +
                  $"{enemyEntries.Count} enemies, {loreEntries.Count} lore entries.");
    }

    private List<T> FindAssetsOfType<T>() where T : Object
    {
        List<T> results = new List<T>();
        string[] ids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
        foreach (string id in ids)
        {
            string path = AssetDatabase.GUIDToAssetPath(id);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null) results.Add(asset); 
        }
        return results; 
    }

    [ContextMenu("Debug Log All EntryIDs")]
    private void LogAllEntryIDs()
    {
        System.Text.StringBuilder sb = new();
        sb.AppendLine("BestiaryDatabase entries:");

        LogCategory(sb, "Items", itemEntries);
        LogCategory(sb, "Areas", areaEntries);
        LogCategory(sb, "Enemies", enemyEntries);
        LogCategory(sb, "Lore", loreEntries);

        Debug.Log(sb.ToString());
    }

    private void LogCategory<T>(System.Text.StringBuilder sb, string label, List<T> entries) where T : BestiaryEntry
    {
        sb.AppendLine($"-- {label} ({entries.Count}) --");
        foreach(T entry in entries)
        {
            string id = string.IsNullOrEmpty(entry.EntryID) ? "<missing ID>" : entry.EntryID;
            sb.AppendLine($"  {id}  ({entry.DisplayName})");
        }
    }
#endif
}