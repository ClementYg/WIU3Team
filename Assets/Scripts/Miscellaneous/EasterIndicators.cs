using UnityEngine;

public class EasterIndicators : MonoBehaviour
{
    [SerializeField] private EventInt onPlatePressedEvent;
    [SerializeField] private Sprite sprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    public void UpdateLight(int index)
    {
        spriteRenderer.sprite = sprite;
        onPlatePressedEvent.Raise(index);
    }
}
