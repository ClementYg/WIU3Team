using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ToolbarBox
{
    public Image boxImage;
    public Image itemImage;
    public TextMeshProUGUI quantityText;

    [HideInInspector] public string itemName = "";
    [HideInInspector] public int itemQuantity;
    [HideInInspector] public bool isOccupied;
}
