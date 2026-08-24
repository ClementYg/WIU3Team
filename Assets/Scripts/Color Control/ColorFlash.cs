using UnityEngine;

public abstract class ColorFlash : MonoBehaviour
{
    [Header("Color Flashing")]
    [SerializeField] protected Color clr;
    [SerializeField] protected float flashInterval = 0.2f;

    protected Color previousColor;

    protected float flashIntervalTimer = 0f;
    protected float flashDurationTimer = 0f;
    protected float flashDuration = 0f;

    protected bool isFlashingByToggle = true;
    protected bool isFlashing = false;

    protected abstract void SetColor();

    // Update is called once per frame
    void Update()
    {
        SetColor();

        if (isFlashing)
        {
            DoColorFlash();
        }
    }

    public void ToggleColorFlash()
    {
        isFlashing = !isFlashing;
        if (isFlashing)
        {
            previousColor = clr;
        }
        else
        {
            clr = previousColor;
        }

        isFlashingByToggle = true;
    }

    public void FlashForDuration(float duration = 1f)
    {
        flashDuration = duration;
        previousColor = clr;
        isFlashingByToggle = false;
        isFlashing = true;
    }

    protected void DoColorFlash()
    {
        flashIntervalTimer += Time.deltaTime;
        if (flashIntervalTimer >= flashInterval)
        {
            clr = GetRandomColor();
            flashIntervalTimer = 0f;
        }

        if (!isFlashingByToggle)
        {
            flashDurationTimer += Time.deltaTime;
            if (flashDurationTimer >= flashDuration)
            {
                clr = previousColor;
                flashDurationTimer = 0f;
                isFlashing = false;
            }
        }
    }

    protected Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
