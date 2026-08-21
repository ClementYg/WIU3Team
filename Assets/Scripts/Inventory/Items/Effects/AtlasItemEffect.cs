using UnityEngine;

[CreateAssetMenu(fileName = "AtlasItemEffect", menuName = "ScriptableObjects/Inventory/Effects/AtlasItemEffect")]
public class AtlasItemEffect : ItemEffect
{
    [Header("Time Switch")]
    [SerializeField] GameObject presentContainer;
    [SerializeField] GameObject pastContainer;

    bool isInPresent = true;

    public override void Use(GameObject user)
    {
        isInPresent = !isInPresent;

        presentContainer.SetActive(isInPresent);
        pastContainer.SetActive(!isInPresent);
    }
}
