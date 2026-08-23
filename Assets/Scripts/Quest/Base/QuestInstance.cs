using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class QuestInstance
{
    [HideInInspector] public int questID;

    [Header("References")]
    public QuestData questData;
    public QuestReward questReward;

    public List<TaskInstance> tasks;

    public bool IsQuestCompleted
    {
        get
        {
            foreach (TaskInstance task in tasks)
            {
                if (!task.IsCompleted) return false;
            }

            return true;
        }
    }
}
