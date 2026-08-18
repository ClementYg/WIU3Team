using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryDisplay : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Inventory inv;
    [SerializeField] GameObject inventoryRow1;
    [SerializeField] GameObject inventoryRow2;

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
}
