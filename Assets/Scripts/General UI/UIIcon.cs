using UnityEngine;

[CreateAssetMenu(fileName = "UIIcon", menuName = "ScriptableObjects/UI/UIIcon")]
public class UIIcon : ScriptableObject
{
    [Header("UI Icon")]
    public Sprite sprite;
    public Vector2 position;
    public Vector2 scale;
}
