using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [HideInInspector] public float damageAmount = 5f;
    public GameObject player;
    private float minSpeed = 2f;
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponentInParent<Health>();
        if (health == null) return;

        health.Damage(damageAmount);
        Destroy(gameObject);
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(this, 2f);        
    }

    private void FixedUpdate()
    {
        Vector2 v = rb.linearVelocity;
        if (v.sqrMagnitude < minSpeed * minSpeed) return;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;
    }
}
