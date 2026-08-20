using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//create instance, just need one to store all of the items
[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/Inventory/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> allItemDatas = new();

    public ItemData GetByID(string itemID)
    {
        return allItemDatas.Find(i => i.itemID == itemID);
    }

    public List<ItemData> GetByType(ItemType type)
    {
        return allItemDatas.FindAll(i => i.itemType == type);
    }

    //unity editor namespace that ensures this code section won't
    //be compiled during game time
#if UNITY_EDITOR
    [ContextMenu("Find ItemData in Project")]
    private void FindAllItemData()
    {
        allItemDatas.Clear();
        string[] ids = AssetDatabase.FindAssets("t:ItemData");
        foreach (string id in ids)
        {
            string path = AssetDatabase.GUIDToAssetPath(id);
            ItemData data = AssetDatabase.LoadAssetAtPath<ItemData>(path);
            if (data != null) allItemDatas.Add(data);
        }
        EditorUtility.SetDirty(this);
        Debug.Log($"ItemDatabase: found {allItemDatas.Count} ItemData assets");
    }

    [ContextMenu("Check For Duplicate IDs")]
    private void CheckDuplicateIds()
    {
        HashSet<string> seen = new();
        foreach (ItemData item in allItemDatas)
        {
            if (item == null) continue;
            if (!seen.Add(item.itemID))
                Debug.LogWarning($"Duplicate itemID '{item.itemID}' found on '{item.name}", item);
        }
    }
#endif
}
