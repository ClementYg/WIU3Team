using UnityEngine;

[CreateAssetMenu(fileName = "Die", menuName = "ScriptableObjects/FSM/Actions/Die")]
public class DieAction : StateAction
{
    public float deathDelay = 1f;

    public override void Act(StateController controller)
    {
        controller.StartCoroutine(DestroyAfterDelay(controller.gameObject));
    }

    private System.Collections.IEnumerator DestroyAfterDelay(GameObject target)
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(target);
    }
}
