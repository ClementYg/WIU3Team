using UnityEngine;

[CreateAssetMenu(fileName = "RaiseMyself", menuName = "ScriptableObjects/FSM/Actions/RaiseMyself")]
public class RaiseMyselfAction : StateAction
{
    public EventGameObject eventGameObject;
    public override void Act(StateController controller)
    {
        eventGameObject.Raise(controller.gameObject);
    }
}
