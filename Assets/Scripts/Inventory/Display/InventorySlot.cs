using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventorySlot
{
    public RectTransform slotRectTransform; // Used for parenting the selection slot
    public Image itemImage;
    public TextMeshProUGUI quantityText;

    [HideInInspector] public string itemName = "";
    [HideInInspector] public int itemQuantity;
    [HideInInspector] public bool isOccupied;
}
