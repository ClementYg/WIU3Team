using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "ScriptableObjects/FSM/State")]
public class State : ScriptableObject
{
    [SerializeField] private List<StateAction> initActions;
    [SerializeField] private List<StateAction> executeActions;
    [SerializeField] private List<StateAction> endActions;
    [SerializeField] private List<StateTransition> transitions;

    public void Init(StateController controller)
    {
        foreach (StateAction action in initActions)
        {
            action.Act(controller);
        }
    }

    public void Execute(StateController controller)
    {
        foreach (StateAction action in executeActions)
        {
            action.Act(controller);
        }
    }

    public void End(StateController controller)
    {
        foreach (StateAction action in endActions)
        {
            action.Act(controller);
        }
    }

    public void CheckTransitions(StateController controller)
    {
        foreach (StateTransition transition in transitions)
        {
            bool decisionSuccess = transition.decision.Decide(controller);
            State nextState = decisionSuccess ? transition.trueState : transition.falseState;

            if (nextState != controller.remainState)
            {
                controller.TransitionToState(nextState);
                return;
            }
        }
    }
}
