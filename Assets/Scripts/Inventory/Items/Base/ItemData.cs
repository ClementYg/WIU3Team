using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Inventory/ItemData")]
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
    public int consumePerUse = 1;

    [Header("Durability")]
    public bool hasDurability = false;
    public int maxDurability = 100;
    public int durabilityPerUse = 1;

    [Header("UseCooldown")]
    public float useCooldown = 0; //how long per use

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
    //means that we don't need to fill itemID ourselves, will autofill based off name

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(itemID) && !string.IsNullOrEmpty(itemName))
        {
            itemID = GenerateIDFromName(itemName);
        }
        //Validation Checks Include:
        //1. Item is Stackable but can only have 1 stack
        //2. Have Durability but doesnt have any durability set.
        if (isStackable && maxStackSize <= 1)
        {
            Debug.LogWarning($"[{name}] isStackable is true but maxStackSize is {maxStackSize}.", this);
        }
        if (maxStackSize > 1 && !isStackable)
        {
            Debug.LogWarning($"[{name}] maxStackSize is {maxStackSize} but isStackable is false", this);
        }
        if (hasDurability && maxDurability <= 0)
        {
            Debug.LogWarning($"[{name}] hasDurability is true but maxDurability is {maxDurability}.", this);
        }
    }

    private string GenerateIDFromName(string name)
    {
        return name.ToLowerInvariant().Replace("-", "_").Replace(" ", "_");
        //return name, with safeguard that iron-sword ==> iron_sword
        //This is allowed to be one line because it processes in 3 steps
        //1. changes name to lowercase, then returns string
        //2. changes any - in string to _, returns new string
        //3. changes any space in string to _, returns final string.
    }
#endif 
}
