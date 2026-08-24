using UnityEngine;
using UnityEngine.UI;

public class BestiaryGrid : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] GameObject lockedOverlay;

    [SerializeField] Button button;

    public void Setup(BestiaryEntry entry, bool IsUnlocked, System.Action onClick)
    {
        iconImage.sprite = entry.Icon;
        iconImage.color = IsUnlocked ? Color.white : Color.black; 
        if (lockedOverlay != null)
        {
            lockedOverlay.SetActive(!IsUnlocked);
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick());
    }
}