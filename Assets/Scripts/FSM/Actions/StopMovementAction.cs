using UnityEngine;

[CreateAssetMenu(fileName = "StopMovement", menuName = "ScriptableObjects/FSM/Actions/StopMovement")]
public class StopMovementAction : StateAction
{
    public override void Act(StateController controller)
    {
        Rigidbody2D rb = controller.GetCached<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }
}
