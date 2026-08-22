using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    //unique PID for every item. 
    public int itemID;
    public ItemData itemData;
    public ItemEffect itemEffect;

    [Header("Realtime Change")]
    public int currentDurability = 0;
    public int stackCount = 1;      // Number of items within one stack
    //is not one time use and no more durability
    float lastUsedTime = -Mathf.Infinity; 

    public bool IsBroken => (itemData.hasDurability && currentDurability <= 0);
    public bool IsFinished => (stackCount <= 0);
    public bool IsOnCooldown => Time.time < lastUsedTime + itemData.useCooldown;
    public float CooldownRemaining => Mathf.Max(0f, (lastUsedTime + itemData.useCooldown) - Time.time);
    public ItemInstance(ItemData itemData, ItemEffect itemEffect = null)
    {
        this.itemData = itemData;
        this.itemEffect = itemEffect;
        currentDurability = (itemData != null && itemData.hasDurability) ? itemData.maxDurability : 0;
    }

    public bool TryUse(GameObject user, ComponentCache userCache)
    {
        if (IsOnCooldown) return false;
        if (IsBroken) return false;
        if (IsFinished) return false;
        if (itemEffect == null) return false;

        itemEffect.Use(user, userCache);
        lastUsedTime = Time.time;

        if (itemData.hasDurability)
        {
            TakeDurabilityDamage(itemData.durabilityPerUse);
        }
        else if (itemData.isStackable)
        {
            ReduceStack(itemData.consumePerUse);
        }

        return true;
    }

    public bool AddToStack(int amount, out int extra)
    {
        extra = 0; 
        //if not stackable just remain as is
        if (!itemData.isStackable) { extra = amount; return false; }
        int total = stackCount + amount; 
        if (total > itemData.maxStackSize)
        {
            extra = total - itemData.maxStackSize;
            total = itemData.maxStackSize;
        }
        stackCount = total;
        //if more than one full stack, put out the excess in extra so can split into 2 stacks
        return true;
    }

    public void ReduceStack(int amount)
    {
        stackCount -= amount;
    }

    //durability functions
    public void TakeDurabilityDamage(int amount)
    {
        if (!itemData.hasDurability) return;
        //dont go below 0 
        currentDurability = Mathf.Max(0, currentDurability - amount);
    }
    public void Repair(int amount)
    {
        if (!itemData.hasDurability) return;
        currentDurability = Mathf.Min(itemData.maxDurability, currentDurability + amount);
    }

    //tooltip for Inventory if you want to add later on 
    //Format will be, e.g:
    //Durability: 2/100 
    //x10
    public string GetTooltip()
    {
        string text = itemData.GetToolTip();
        if (itemData.hasDurability)
        {
            text += $"\nDurability: {currentDurability}/{itemData.maxDurability}";
        }
        if (itemData.isStackable && stackCount > 1)
        {
            text += $"\nx{stackCount}";
        }
        return text; 
    }
}
