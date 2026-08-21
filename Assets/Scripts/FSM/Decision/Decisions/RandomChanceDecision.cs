using UnityEngine;

[CreateAssetMenu(fileName = "RandomChance", menuName = "ScriptableObjects/FSM/Decisions/RandomChance")]
public class RandomChanceDecision : StateDecision
{
    [Range(0f, 1f)] public float chance = 0.5f;

    public override bool Decide(StateController controller)
    {
        return Random.value < chance;
    }
}
