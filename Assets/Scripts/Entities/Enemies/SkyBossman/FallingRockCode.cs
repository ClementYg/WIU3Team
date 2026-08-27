using UnityEngine;

public class FallingRockCode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {
            this.gameObject.transform.localScale *= 0.9f;
        }
    }
}
