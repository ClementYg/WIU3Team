using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
    [SerializeField] private PressurePlate plate;
    [SerializeField] private string[] layerNames;

    void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < layerNames.Length; i++)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(layerNames[i])) plate.Press();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < layerNames.Length; i++)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(layerNames[i])) plate.Release();
        }
    }
}
