using UnityEngine;
using UnityEngine.UI;

public class ButtonColorFlash : ColorFlash
{
    [Header("Button Color Flash")]
    [SerializeField] Button button;

    protected override void SetColor()
    {
        ColorBlock clrBlock = button.colors;
        clrBlock.normalColor = clr;
        button.colors = clrBlock;
    }
}
