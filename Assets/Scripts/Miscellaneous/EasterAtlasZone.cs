using UnityEngine;
using UnityEngine.Rendering;

public class EasterAtlasZone : MonoBehaviour
{
    [SerializeField] private EventInt onPlatePressedEvent;
    [SerializeField] private bool[] plateIndices;
    [SerializeField] private MultiPressPuzzle puzzle;
    [SerializeField] private AtlasRestrictedZone zone;

    private void OnEnable()
    {
        onPlatePressedEvent.Subscribe(OnPlatePressed);
    }

    private void OnDisable()
    {
        onPlatePressedEvent.Unsubscribe(OnPlatePressed);
    }

    private void Start()
    {
        puzzle.StartPuzzle("easter_multipress");
    }

    private void OnPlatePressed(int index)
    {
        plateIndices[index] = true;
        bool allPressed = true;
        foreach (bool isPressed in plateIndices)
        {
            if (!isPressed) allPressed = false;
        }

        if (allPressed)
        {
            Destroy(zone);
        }
    }
}
