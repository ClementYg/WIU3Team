using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryDisplay : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Inventory inv;
    [SerializeField] GameObject inventoryRow1;
    [SerializeField] GameObject inventoryRow2;

    
    // Track the first empty slot index
    int emptySlotIndex = 0;

    // Used to track the current storage states
    int currSlotCapacity = 0;
    public readonly int maxSlotCapacity = 12;
    bool isDisplayFull = false;

    public int CurrSlotCapacity => currSlotCapacity;
    public bool IsDisplayFull => isDisplayFull; // Checked by inventory script before AddItem() is called

    bool isDisplaying = false;

    // Update is called once per frame
    void Update()
    {
        bool isToggleDisplayPressed = InputSystem.actions["Toggle Inventory Display"].WasPressedThisFrame();
        if (isToggleDisplayPressed)
        {
            isDisplaying = !isDisplaying;

            if (isDisplaying)
            {
                inventoryRow1.SetActive(true);
                inventoryRow2.SetActive(true);
            }
            if (!isDisplaying)
            {
                inventoryRow1.SetActive(false);
                inventoryRow2.SetActive(false);
            }
        }
    }

    public void AddItem(ItemInstance item)
    {
    }
}
