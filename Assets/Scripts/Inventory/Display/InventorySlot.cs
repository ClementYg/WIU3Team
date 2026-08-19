using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Inventory Slot")]
    public RectTransform slotRectTransform; // Used for parenting the selection slot
    public Image itemImage;
    public TextMeshProUGUI quantityText;
    public bool isOccupied;

    [Header("Event Channels")]
    [SerializeField] EventInventorySlot OnInventoryClickEvent;

    [HideInInspector] public string itemName = null;
    [HideInInspector] public int itemQuantity;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnInventoryClickEvent.Raise(this);
    }
}
