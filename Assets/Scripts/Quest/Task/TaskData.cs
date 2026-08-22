using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "ScriptableObjects/Quests/TaskData")]
public class TaskData : ScriptableObject
{
    [Header("Task")]
    [TextArea(1, 2)] public string instruction;
    public TaskType taskType;
    public int requiredQuantity;
}
