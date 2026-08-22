using UnityEngine;

[System.Serializable]
public class TaskInstance : ISerializationCallbackReceiver
{
    public TaskData taskData;

    [Header("Runtime Status")]
    public int currentQuantity = 0;
    public bool IsCompleted => (currentQuantity >= taskData.requiredQuantity);

    public void OnAfterDeserialize()
    {
        currentQuantity = 0;
    }

    public void OnBeforeSerialize()
    {

    }
}
