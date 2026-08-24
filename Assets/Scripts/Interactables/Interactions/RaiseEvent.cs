using UnityEngine;

[CreateAssetMenu(fileName = "RaiseEvent", menuName = "ScriptableObjects/Interaction/RaiseEvent")]
public class RaiseEvent : Interaction
{
    [SerializeField] private EventVoid eventChannel;
    
    public override void Do()
    {
        eventChannel.Raise();
    }
}
