using UnityEngine;

public class ChargeCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.gameObject.GetComponent<EnemyBlackboard>().chargeProgress = 1;
        }
    }
}
