using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemData itemData;
    public ItemEffect itemEffect;

    [Header("Realtime Change")]
    public int currentDurability = 0;
    public int stackCount = 1;      // Number of items within one stack
    //is not one time use and no more durability

    public bool IsBroken => (itemData.hasDurability && currentDurability <= 0);
    public bool IsFinished => (stackCount <= 0);

    public ItemInstance(ItemData itemData, ItemEffect itemEffect = null)
    {
        this.itemData = itemData;
        this.itemEffect = itemEffect;
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
