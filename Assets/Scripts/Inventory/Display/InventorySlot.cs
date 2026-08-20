using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Inventory Slot")]
    public RectTransform slotRectTransform; // Used for parenting the selection slot
    public SlotUI UI;

    [Header("Event Channels")]
    [SerializeField] EventInventorySlot OnInventoryClickEvent;

    [HideInInspector] public string itemName = null;
    [HideInInspector] public int itemQuantity;

    public bool IsOccupied
    {
        get
        {
            if (UI == null || itemName == null || itemQuantity == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnInventoryClickEvent.Raise(this);
    }

    public void SetUI(Sprite sprite, int itemQuantity)
    {
        this.itemQuantity = itemQuantity;
        UI.itemImage.sprite = sprite;
        UI.itemImage.enabled = true;
        UI.quantityText.text = itemQuantity.ToString();
        UI.quantityText.enabled = true;
    }

    public void Clear()
    {
        itemQuantity = 0;
        UI.itemImage.sprite = null;
        UI.itemImage.enabled = false;
        UI.quantityText.text = itemQuantity.ToString();
        UI.quantityText.enabled = false;
        UI.itemImage.transform.localPosition = new Vector3(0f, 0f, 0f);
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
