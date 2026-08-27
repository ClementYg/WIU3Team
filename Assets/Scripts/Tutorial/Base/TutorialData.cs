using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TutorialData", menuName = "ScriptableObjects/Tutorial/TutorialData")]
public class TutorialData : ScriptableObject
{
    [Header("Tutorial Data")]
    public List<StepData> steps;

    [Header("Add Item")]
    public ItemData itemData;
    public ItemEffect itemEffect;
    public bool shouldAddItem = false;

    public void AddItemAtSlot()
    {
        if (shouldAddItem == false) return;

        if (itemData != null && itemEffect != null)
        {
            Inventory.Instance.AddItemAtSlot(itemData, itemEffect, 1, 0, 0);
        }
    }
}
