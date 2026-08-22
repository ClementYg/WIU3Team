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
        GameObject inventory = GameObject.FindWithTag("Inventory");
        if (inventory == null)
        {
            Debug.LogError("TimeSwitchReferences: Failed to assign references.");
            return;
        }

        if (inventory.TryGetComponent<CameraShaker>(out CameraShaker shaker))
        {
            CmrShaker = shaker;
        }
        else
        {
            Debug.LogError("TimeSwitchReferences: Failed to assign references.");
            return;
        }
    }
}
