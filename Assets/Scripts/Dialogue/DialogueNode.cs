using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "ScriptableObjects/Dialogue/DialogueNode")]
public class DialogueNode : ScriptableObject
{
    [Header("Speaker")]
    public CharacterData speaker;

    [Header("Text")]
    [TextArea(3, 6)]
    public string text;

    [Header("Flow")]
    public DialogueNode nextNode;
    public DialogueChoice[] choices;

    private void OnValidate()
    {
        if (choices != null && choices.Length > 3)
        {
            Debug.LogWarning($"[DialogueNode] {name} has more than 3 choices", this);
            System.Array.Resize(ref choices, 3);
        }
    }
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public DialogueNode nextNode;

    private void OnValidate()
    {

    }
}