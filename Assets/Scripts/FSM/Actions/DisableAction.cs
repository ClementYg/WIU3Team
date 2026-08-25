using UnityEngine;

[CreateAssetMenu(fileName = "Disable", menuName = "ScriptableObjects/FSM/Actions/Disable")]
public class DisableAction : StateAction
{
    public override void Act(StateController controller)
    {
        Collider2D collider = controller.GetCached<Collider2D>();
        if (collider != null) collider.enabled = false;

        Rigidbody2D rb = controller.GetCached<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        controller.enabled = false;
    }
}
