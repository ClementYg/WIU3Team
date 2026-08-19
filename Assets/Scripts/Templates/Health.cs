using UnityEngine;

public class Health : MonoBehaviour
{
    public float HP, MaxHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float Damage)
    {
        HP -= Damage;
        
        if (HP <= float.Epsilon)
        {
            Destroy(this);
        }
        
    }
}
