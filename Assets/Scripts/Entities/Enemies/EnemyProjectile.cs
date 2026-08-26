using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    public float lifeSpan = 3;
    void Start()
    {
        
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
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
            collision.gameObject.GetComponent<Lives>().Damage();
            Destroy(this.gameObject);
        }
    }
}
