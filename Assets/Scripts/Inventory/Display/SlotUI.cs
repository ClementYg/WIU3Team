using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SlotUI
{
    public Image itemImage;
    public TextMeshProUGUI quantityText;

    public void SetUI(Transform newTransform, Sprite newSprite, string newText, bool shouldEnable)
    {
        SetSprite(newSprite, shouldEnable);
        itemImage.transform.SetParent(newTransform);

        SetText(newText, shouldEnable);
        quantityText.transform.SetParent(newTransform);
    }

    private void SetSprite(Sprite newSprite, bool shouldEnable)
    {
        itemImage.sprite = newSprite;
        itemImage.enabled = shouldEnable;
    }

    private void SetText(string newText, bool shouldEnable)
    {
        quantityText.text = newText;
        quantityText.enabled = shouldEnable;
    }
}
