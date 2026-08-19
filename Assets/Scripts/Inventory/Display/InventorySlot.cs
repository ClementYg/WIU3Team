using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [Header("Inventory Slot")]
    public RectTransform slotRectTransform; // Used for parenting the selection slot
    public Image itemImage;
    public TextMeshProUGUI quantityText;
    public bool isOccupied;

    [HideInInspector] public string itemName = null;
    [HideInInspector] public int itemQuantity;
}
