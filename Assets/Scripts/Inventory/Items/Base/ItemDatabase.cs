using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//create instance, just need one to store all of the items
[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/Inventory/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> allItems = new();

    public ItemData GetByID(string itemID)
    {
        return allItems.Find(i => i.itemID == itemID);
    }

    public List<ItemData> GetByType(ItemType type)
    {
        return allItems.FindAll(i => i.itemType == type);
    }

    //unity editor namespace that ensures this code section won't
    //be compiled during game time
#if UNITY_EDITOR
    [ContextMenu("Find ItemData in Project")]
    private void FindAllItemData()
    {
        allItems.Clear();
        string[] ids = AssetDatabase.FindAssets("t:ItemData");
        foreach (string id in ids)
        {
            string path = AssetDatabase.GUIDToAssetPath(id);
            ItemData data = AssetDatabase.LoadAssetAtPath<ItemData>(path);
            if (data != null) allItems.Add(data);
        }
        EditorUtility.SetDirty(this);
        Debug.Log($"ItemDatabase: found {allItems.Count} ItemData assets");
    }

    [ContextMenu("Check For Duplicate IDs")]
    private void CheckDuplicateIds()
    {
        HashSet<string> seen = new();
        foreach (ItemData item in allItems)
        {
            if (item == null) continue;
            if (!seen.Add(item.itemID))
                Debug.LogWarning($"Duplicate itemID '{item.itemID}' found on '{item.name}", item);
        }
    }
#endif
}
