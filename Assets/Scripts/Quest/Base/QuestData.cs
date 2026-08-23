using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/Quests/QuestData")]
public class QuestData : ScriptableObject
{
    [Header("Details")]
    public string questName;
    public QuestSource source;

    [Header("Requirements")]
    public List<QuestRequirement> requirements = new();
}
