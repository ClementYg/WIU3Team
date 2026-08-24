using UnityEngine;

[CreateAssetMenu(fileName = "QuestReward", menuName = "ScriptableObjects/Quests/QuestReward")]
public class QuestReward : ScriptableObject
{
    [Header("Reward")]
    public ItemData itemData;
    public ItemEffect itemEffect;
    public int quantityToReward;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (itemData == null || itemEffect == null)
        {
            Debug.LogWarning("QuestReward has missing reference(s). Reward name: " + itemData.itemName);
        }
    }
#endif
}
