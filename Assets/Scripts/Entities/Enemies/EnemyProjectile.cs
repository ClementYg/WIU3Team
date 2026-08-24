using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    private float lifeSpan = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeSpan <= 0)
        {
            Destroy(this.gameObject);
        }
        lifeSpan -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == playerLayer.value)
        {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<Health>().Damage(1);
            Destroy(this.gameObject);
        }
    }
}
