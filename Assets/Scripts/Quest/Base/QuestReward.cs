using UnityEngine;

[CreateAssetMenu(fileName = "QuestReward", menuName = "ScriptableObjects/Quests/QuestReward")]
public class QuestReward : ScriptableObject
{
    [Header("Reward")]
    [Delayed] public string rewardName;
    public ItemData itemReward;
    public int quantityToReward;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (itemReward == null)
        {
            Debug.LogWarning("QuestReward must have ItemData as a reference. Reward name: " + rewardName);
        }
    }
#endif
}
