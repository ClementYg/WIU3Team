using UnityEngine;

public class TimeSwitchReferences : MonoBehaviour
{
    [Header("Time States")] 
    [field: SerializeField] public GameObject Present { get; private set; }
    [field: SerializeField] public GameObject Past { get; private set; }

    [Header("Transition Sequence")]
    [field: SerializeField] public ColorChannel PrsntClrChannel { get; private set; }
    [field: SerializeField] public ColorChannel PstClrChannel { get; private set; }

    public CameraShaker CmrShaker { get; private set; }

    public void AssignReferences()
    {
        // Look for the camera on the player first
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            if (player.TryGetComponent<CameraShaker>(out CameraShaker shaker))
            {
                CmrShaker = shaker;
                return;
            }
            else
            {
                Debug.LogError("TimeSwitchReferences: Failed to assign references.");
                return;
            }
        }

        // Failed, now just find any game object that has the Camera Shaker script
        CmrShaker = FindAnyObjectByType<CameraShaker>();
        if (CmrShaker == null)
        {
            Debug.LogError("TimeSwitchReferences: Failed to assign references.");
            return;
        }
    }
}
