using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Inventory Slot")]
    public RectTransform slotRectTransform; // Used for parenting the selection slot
    public SlotUI UI;
    public bool isOccupied;

    [Header("Event Channels")]
    [SerializeField] EventInventorySlot OnInventoryClickEvent;

    [HideInInspector] public string itemName = null;
    [HideInInspector] public int itemQuantity;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnInventoryClickEvent.Raise(this);
    }

#if UNITY_EDITOR
    [ContextMenu("Find All References")]
    private void FindAllReferences()
    {
        if (TryGetComponent<RectTransform>(out RectTransform rctTransform))
        {
            slotRectTransform = rctTransform;
        }

        Transform imageChild = transform.GetChild(0);
        if (imageChild.TryGetComponent<Image>(out Image img))
        {
            UI.itemImage = img;
        }

        Transform textChild = transform.GetChild(1);
        if (textChild.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
        {
            UI.quantityText = text;
        }
    }
#endif
}
