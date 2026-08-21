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

    // Keep a reference to the item instance that will be placed at this slot
    [System.NonSerialized] public ItemInstance itemDisplayed = null;

    public bool IsOccupied
    {
        get
        {
            return (itemDisplayed != null);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("InventorySlot: detected mouse click");
        OnInventoryClickEvent.Raise(this);
    }

    public void AddToStack(ref int amountToAdd)
    {
        itemDisplayed.AddToStack(amountToAdd, out int excess);
        UI.quantityText.text = itemDisplayed.stackCount.ToString();

        amountToAdd = excess;
    }

    public void ReduceStack(int amountToReduce)
    {
        itemDisplayed.ReduceStack(amountToReduce);
        UI.quantityText.text = itemDisplayed.stackCount.ToString();
    }

    public void SetSlot(ItemInstance item)
    {
        itemDisplayed = item;
        SetUI(item.itemData.itemImage, item.stackCount);
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
        UI.itemImage.transform.localPosition = Vector3.zero;

        UI.quantityText.text = null;
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
