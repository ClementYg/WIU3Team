using UnityEngine;

public class SprayAttack : MonoBehaviour
{
    public int toSpray;
    public float SprayCD;
    public GameObject sprayCloud;

    // Update is called once per frame
    void Update()
    {
        SprayCD -= Time.deltaTime;
        if (toSpray > 0 && SprayCD < 0)
        {
            var temp = Instantiate(sprayCloud, transform.position, transform.rotation);
            temp.GetComponent<Rigidbody2D>().AddForceX(Random.Range(-0.5f,0.5f), ForceMode2D.Impulse);
            SprayCD = 0.2f;
            toSpray--;
        }
    }
}
