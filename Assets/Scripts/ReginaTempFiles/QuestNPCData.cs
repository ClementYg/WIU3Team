using UnityEngine;

[CreateAssetMenu(fileName = "QuestNPCData", menuName = "Scriptable Objects/NPCs/QuestNPCData")]
public class QuestNPCData : NPCData
{
    int questIndex;

    public void AssignQuest(QuestInstance toAssign, ComponentCache questCache)
    {
        QuestManager questMan = questCache.Get<QuestManager>();
        if (questMan == null) return;

        questIndex = questMan.AssignQuest(toAssign);
    }
}
