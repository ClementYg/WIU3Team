using UnityEngine;

// This script serves to establish a connection between a GameObject and the ColorChannel shader.
public class ColorChannel : MonoBehaviour
{
    [Header("Renderer")]
    [SerializeField] Renderer rndrr;
    [SerializeField] Color clr;

    Material mtrl;
    int mainColorID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mtrl = rndrr.material;
        mainColorID = Shader.PropertyToID("_MainColor");
    }

    // Update is called once per frame
    void Update()
    {
        mtrl.SetColor(mainColorID, clr);
    }
}
