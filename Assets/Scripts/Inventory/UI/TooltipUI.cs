using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : Singleton<TooltipUI>
{
    [SerializeField] RectTransform tooltipRect;
    [SerializeField] TextMeshProUGUI tooltipText;
    [SerializeField] Vector2 padding = new Vector2(15f, -15f);

    protected override void Awake()
    {
        base.Awake();
        if (tooltipText != null) tooltipText.raycastTarget = false;
        if (tooltipRect.TryGetComponent(out Image bgImage)) bgImage.raycastTarget = false;
        Hide();
    }

    public void Show(string text, Vector2 anchorPos)
    {
        tooltipText.text = text;
        tooltipRect.gameObject.SetActive(true);
        tooltipRect.position = anchorPos + padding;
    }

    public void Hide()
    {
        tooltipRect.gameObject.SetActive(false);
    }
}