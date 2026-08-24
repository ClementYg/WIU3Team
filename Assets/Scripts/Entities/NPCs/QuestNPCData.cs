using UnityEngine;

[CreateAssetMenu(fileName = "QuestNPCData", menuName = "ScriptableObjects/NPCs/QuestNPCData")]
public class QuestNPCData : NPCData
{
    int questIndex;

    public void AssignQuest(QuestInstance toAssign, ComponentCache questCache)
    {
        QuestSystem questSys = questCache.Get<QuestSystem>();
        if (questSys == null) return;

        questIndex = questSys.AssignQuest(toAssign);
    }
}
