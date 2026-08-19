using UnityEngine;

[CreateAssetMenu(fileName = "StateAction", menuName = "ScriptableObjects/FSM/Action")]
public abstract class StateAction : ScriptableObject
{
    public abstract void Act(StateController controller);
}
