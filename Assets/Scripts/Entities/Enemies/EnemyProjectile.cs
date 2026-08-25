using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    private float lifeSpan = 3;
    void Start()
    {
        
    }

    private void Awake()
    {
        Debug.Log("Shoot");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<Health>().Damage(1);
            Destroy(this.gameObject);
        }
    }
}
