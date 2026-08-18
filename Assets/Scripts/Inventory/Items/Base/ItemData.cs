using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Identifiers")]
    public string itemID; //unique PID for database
    [Delayed] public string itemName;
    public Sprite itemImage;
    public ItemType itemType;

    [Header("Descriptors")]
    public string shortDescription; //used for toolbar hover later maybe
    public string loreDescription; //bestiary/index description 

    [Header("Stacking")]
    public bool isStackable = false;
    public int maxStackSize = 1;

    [Header("Durability")]
    public bool hasDurability = false;
    public int maxDurability = 100;

    [Header("Stats")]
    public List<StatModifier> statModifiers = new();

    //virtual so can change in specific item Datas
    public virtual string GetToolTip()
    {
        string text = $"<b>{itemName}</b>";
        if (!string.IsNullOrEmpty(shortDescription))
        {
            text += $"\n{shortDescription}";
        }

        foreach (StatModifier statMod in statModifiers)
        {
            text += $"\n{statMod.GetDisplayText()}";
        }
        return text;
    }

#if UNITY_EDITOR
    //unity function called when open inspector etc
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(itemID) && !string.IsNullOrEmpty(itemName))
        {
            itemID = GenerateIDFromName(itemName);
        }

        if (isStackable && maxStackSize <= 1)
        {
            Debug.LogWarning($"[{name}] isStackable is true but maxStackSize is {maxStackSize}.", this);
        }
        if (hasDurability && maxDurability <= 0)
        {
            Debug.LogWarning($"[{name}] hasDurability is true but maxDurability is {maxDurability}.", this);
        }
    }

    private string GenerateIDFromName(string name)
    {
        return name.ToLowerInvariant().Replace("-", "_");
        //return name, with safeguard that iron-sword ==> iron_sword
    }
#endif 
}
