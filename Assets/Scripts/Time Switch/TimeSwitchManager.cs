using UnityEngine;

public class TimeSwitchManager : MonoBehaviour
{
    [Header("Time Switch")]
    [SerializeField] GameObject presentContainer;
    [SerializeField] GameObject pastContainer;

    bool isInPresent = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // At the start, player should be in the present
        UpdateContainers();
    }

    public void UseAtlas()
    {
        // Toggle the time state
        isInPresent = !isInPresent;
        UpdateContainers();
    }

    private void UpdateContainers()
    {
        presentContainer.SetActive(isInPresent);
        pastContainer.SetActive(!isInPresent);
    }
}
