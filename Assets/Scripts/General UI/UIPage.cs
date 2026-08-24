using UnityEngine;

[CreateAssetMenu(fileName = "UIPage", menuName = "ScriptableObjects/UI/UIPage")]
public class UIPage : ScriptableObject
{
    [Header("UI Page")]
    public Color pageColor;
    public ColoredText header;
    public ColoredText body;
}
