using UnityEngine;

public class ColorChannel : MonoBehaviour
{
    // Note: This script serves to establish a connection between
    // a GameObject and the ColorChannel shader.

    [Header("Color Channel")]
    [SerializeField] Renderer rndrr;
    [SerializeField] Color clr;

    [Header("Color Flashing")]
    [SerializeField] float flashInterval = 0.2f;
    Color previousColor;
    float flashTimer = 0f;
    bool isFlashing = false;

    Material mtrl;
    int mainColorID;

    private void Awake()
    {
        mtrl = rndrr.material;
        mainColorID = Shader.PropertyToID("_MainColor");
    }

    // Update is called once per frame
    void Update()
    {
        mtrl.SetColor(mainColorID, clr);

        if (isFlashing)
        {
            DoColorFlash();
        }
    }

    public void StartColorFlash()
    {
        previousColor = clr;
        isFlashing = true;
    }

    public void StopColorFlash()
    {
        clr = previousColor;
        isFlashing = false;
    }

    private void DoColorFlash()
    {
        flashTimer += Time.deltaTime;
        if (flashTimer >= flashInterval)
        {
            clr = new Color(Random.value, Random.value, Random.value);
            flashTimer = 0f;
        }
    }
}
