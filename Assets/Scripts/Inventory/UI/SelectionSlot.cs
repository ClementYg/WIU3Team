using UnityEngine;
using UnityEngine.UI;

public class SelectionSlot : MonoBehaviour
{
    [Header("Selection Slot")]
    // Store a reference to the image
    [SerializeField] Image selectionImage;

    InventorySlot selectedSlot;
    public InventorySlot SelectedSlot => selectedSlot;

    public void MoveSelectionSlot(InventorySlot newSlot)
    {
        // Visually move the image
        selectionImage.rectTransform.SetParent(newSlot.slotRectTransform, false);
        selectionImage.rectTransform.localPosition = Vector3.zero;

        // Update the reference
        selectedSlot = newSlot;
    }
}
