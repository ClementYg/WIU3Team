using UnityEngine;

public class ColorChannel : ColorFlash
{
    // Note: This script serves to establish a connection between
    // a GameObject and the ColorChannel shader.

    [Header("Color Channel")]
    [SerializeField] Renderer rndrr;

    Material mtrl;
    int mainColorID;

    private void Awake()
    {
        mtrl = rndrr.material;
        mainColorID = Shader.PropertyToID("_MainColor");
    }

    protected override void SetColor()
    {
        mtrl.SetColor(mainColorID, clr);
    }
}
