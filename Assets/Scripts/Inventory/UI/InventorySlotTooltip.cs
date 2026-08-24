using UnityEngine;
using UnityEngine.EventSystems;

//Be placed on same GO as InventorySlot
public class InventorySlotTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventorySlot slot;

    private void Awake()
    {
        slot = GetComponent<InventorySlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!slot.IsOccupied) return;
        TooltipUI.Instance.Show(slot.itemDisplayed.GetTooltip(), eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.Instance.Hide();
    }
}