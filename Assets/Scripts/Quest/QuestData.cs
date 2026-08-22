using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/Quests/QuestData")]
public class QuestData : ScriptableObject
{
    [Header("Details")]
    public string questName;
    [TextArea(2, 10)] public string questContext;

    [Header("Requirements")]
    public List<QuestRequirement> requirements = new();
}
