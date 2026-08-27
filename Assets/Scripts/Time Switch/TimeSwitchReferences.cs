using UnityEngine;
using System.Collections.Generic;

public class TimeSwitchReferences : MonoBehaviour
{
    [Header("Time States")] 
    [field: SerializeField] public GameObject Present { get; private set; }
    [field: SerializeField] public GameObject Past { get; private set; }

    [Header("Transition Sequence")]
    [field: SerializeField] public List<ColorChannel> PrsntClrChannels { get; private set; }
    [field: SerializeField] public List<ColorChannel> PstClrChannels { get; private set; }

#if UNITY_EDITOR
    [ContextMenu("Find All References")]
    private void FindAllReferences()
    {
        // Clear the references
        PrsntClrChannels.Clear();
        PstClrChannels.Clear();

        // Find all references
        GameObject presentContainer = GameObject.Find("Present Container");
        if (presentContainer != null)
        {
            Present = presentContainer;
        }

        GameObject pastContainer = GameObject.Find("Past Container");
        if (pastContainer != null)
        {
            Past = pastContainer;
        }

        ColorChannel[] presentColorChannels = presentContainer.GetComponents<ColorChannel>();
        ColorChannel[] pastColorChannels = pastContainer.GetComponents<ColorChannel>();

        PrsntClrChannels = new List<ColorChannel>(presentColorChannels);
        PstClrChannels = new List<ColorChannel>(pastColorChannels);
    }
#endif
}
