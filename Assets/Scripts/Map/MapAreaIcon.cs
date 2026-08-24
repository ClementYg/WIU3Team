using UnityEngine;
using UnityEngine.UI;

public class MapAreaIcon : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] RectTransform rectTransform;

    public void Setup(AreaEntryData area, bool discovered)
    {
        if (area.regionSprite != null)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            iconImage.sprite = area.regionSprite;
        }
        else
        {
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = area.mapPosition;
            iconImage.sprite = area.icon;
            iconImage.SetNativeSize();
        }

        gameObject.SetActive(discovered);
    }
}