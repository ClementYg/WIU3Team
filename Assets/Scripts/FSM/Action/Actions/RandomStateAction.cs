using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomState", menuName = "ScriptableObjects/FSM/Actions/RandomState")]
public class RandomStateAction : StateAction
{
    [System.Serializable]
    private class WeightedState
    {
        public State state;
        [Min(0f)] public float weight = 1f;
    }

    [SerializeField] private List<WeightedState> options;

    public override void Act(StateController controller)
    {
        float total = 0f;
        foreach (var option in options) total += option.weight;

        float roll = Random.Range(0f, total);
        float cumulative = 0f;

        foreach (var option in options)
        {
            cumulative += option.weight;
            if (roll <= cumulative)
            {
                controller.TransitionToState(option.state);
                return;
            }
        }
    }
}