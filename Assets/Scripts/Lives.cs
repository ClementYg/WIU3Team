using UnityEngine;

public class Lives : MonoBehaviour
{
    public int MaxLives, CurrLives;
    private float iFrames;
    public SpriteRenderer spriteRend;
    public EventInt OnPlayerHPChange;
    void Start()
    {
        CurrLives = MaxLives;
        spriteRend = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        iFrames -= Time.deltaTime;
        if (iFrames > 0)
        {
            var currColor = spriteRend.color;
            currColor.b = 0.5f;
            currColor.g = 0.5f;
            currColor.a = 0.7f;
            spriteRend.color = currColor;
        }
        else
        {
            var currColor = Color.white;
            currColor.a = 1f;
            spriteRend.color = currColor;
        }
    }

    public void Damage()
    {
        if (iFrames <= 0)
        {
            CurrLives--;
            iFrames = 1.0f;
            OnPlayerHPChange.Raise(CurrLives);
        }
        Debug.Log("Hit");
    }
}
