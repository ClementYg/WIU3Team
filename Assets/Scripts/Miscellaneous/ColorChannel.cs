using UnityEngine;

// This script serves to establish a connection between a GameObject and the ColorChannel shader.
public class ColorChannel : MonoBehaviour
{
    [Header("Renderer")]
    [SerializeField] Renderer rndrr;
    [SerializeField] Color clr;

    Material mtrl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mtrl = rndrr.material;
    }

    // Update is called once per frame
    void Update()
    {
        mtrl.SetColor("_MainColor", clr);
    }
}
