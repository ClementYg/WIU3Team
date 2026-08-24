using UnityEngine;

[CreateAssetMenu(fileName = "QuestNPCData", menuName = "ScriptableObjects/NPCs/QuestNPCData")]
public class QuestNPCData : NPCData
{
    public int AssignQuest(QuestInstance toAssign)
    {
        return QuestSystem.Instance.AssignQuest(toAssign);
    }

    public bool CompleteQuest(int questID)
    {
        return QuestSystem.Instance.CompleteQuest(questID);
    }
}
