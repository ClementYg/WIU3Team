using UnityEngine;

[CreateAssetMenu(fileName = "StateDecision", menuName = "ScriptableObjects/FSM/Decision")]
public abstract class StateDecision : ScriptableObject
{
    public abstract bool Decide(StateController controller);
}
