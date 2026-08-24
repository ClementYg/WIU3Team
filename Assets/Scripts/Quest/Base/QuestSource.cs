using UnityEngine;

public abstract class QuestSource : ScriptableObject
{
    public SourceType type;
    [TextArea(2, 10)] public string questContext;
}
