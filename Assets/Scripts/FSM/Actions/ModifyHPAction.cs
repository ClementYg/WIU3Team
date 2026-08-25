using UnityEngine;

[CreateAssetMenu(fileName = "ModifyHP", menuName = "ScriptableObjects/FSM/Actions/ModifyHP")]
public class ModifyHPAction : StateAction
{
    public enum Effect { Damage, Heal }

    [SerializeField] private Effect effect;
    [SerializeField] private float amount;

    public override void Act(StateController controller)
    {
        Health health = controller.GetCached<Health>();

        switch (effect)
        {
        case Effect.Damage:
            health.Damage(amount);
            break;
        case Effect.Heal:
            health.Heal(amount);
            break;
        }
    }
}
