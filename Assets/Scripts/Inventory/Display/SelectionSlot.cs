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

        Vector3 newPos = selectionImage.rectTransform.localPosition;
        newPos = Vector3.zero;
        selectionImage.rectTransform.localPosition = newPos;

        // Update the reference
        selectedSlot = newSlot;
    }
}
