using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Inventory Slot")]
    public ItemInstance itemDisplayed = null;
    public RectTransform slotRectTransform; // Used for parenting the selection slot
    public SlotUI UI;

    [Header("Event Channels")]
    [SerializeField] EventInventorySlot OnInventoryClickEvent;

    [HideInInspector] public int slotID;

    public bool IsOccupied
    {
        get
        {
            return (itemDisplayed != null);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnInventoryClickEvent.Raise(this);
    }

    public void SetUI(Sprite sprite, int itemQuantity)
    {
        UI.itemImage.sprite = sprite;
        UI.itemImage.enabled = true;
        UI.quantityText.text = itemQuantity.ToString();
        UI.quantityText.enabled = true;
    }

    public void ClearUI()
    {
        UI.itemImage.sprite = null;
        UI.itemImage.enabled = false;
        UI.itemImage.transform.localPosition = new Vector3(0f, 0f, 0f);
        UI.quantityText.text = itemDisplayed.stackCount.ToString();
        UI.quantityText.enabled = false;
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
