using UnityEngine;

[CreateAssetMenu(fileName = "RaiseEvent", menuName = "ScriptableObjects/FSM/Actions/RaiseEvent")]
public class RaiseEventAction : StateAction
{
    public EventVoid eventVoid;

    public override void Act(StateController controller)
    {
        eventVoid.Raise();
    }
}
