using System.Collections;
using UnityEngine;

[System.Serializable]
public class CutsceneDialogueStep : CutsceneStep
{
    public DialogueConversation conversation;

    public override IEnumerator Execute()
    {
        bool finished = false;
        void OnFinished() => finished = true;

        CutsceneManager.Instance.onDialogueEndedEvent.Subscribe(OnFinished);
        DialogueManager.Instance.StartConversation(conversation);

        yield return new WaitUntil(() => finished);
        CutsceneManager.Instance.onDialogueEndedEvent.Unsubscribe(OnFinished);
    }
}
