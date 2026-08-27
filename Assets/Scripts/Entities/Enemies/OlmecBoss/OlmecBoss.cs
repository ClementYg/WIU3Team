using UnityEngine;
using UnityEngine.Splines;

public class OlmecBoss : MonoBehaviour
{
    enum BOSSSTATES
    {
        REST,
        SPRAY,
        LAZER,
        POUND = 3
    }

    private BOSSSTATES currState = 0;
    public GameObject Emerald, Mouth;
    public LazerAttack Lazer;
    public SprayAttack Spray;
    public EnemyBlackboard blackboard;
    public Rigidbody2D rb;
    public SpriteRenderer SpriteRend;
    public Sprite Angry, Rest;
    public Vector3 targetPos;
    public bool moveLock;
    public GameObject attackPoint;
    public float poundAttackTickRate = -1;
    //private Coroutine StateTimer;

    private void Awake()
    {
        blackboard = gameObject.GetComponent<EnemyBlackboard>();
        SpriteRend = gameObject.GetComponent<SpriteRenderer>();
        Lazer = gameObject.GetComponentInChildren<LazerAttack>(true);
        Emerald = Lazer.gameObject;
        Spray = gameObject.GetComponentInChildren<SprayAttack>(true);
        Mouth = Spray.gameObject;
        rb = gameObject.GetComponent<Rigidbody2D>();
        Random.InitState((int)System.DateTime.Now.Ticks);
        changeState(BOSSSTATES.REST);
    }

    void Update()
    {
        if (moveLock)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 1);
        }

        if (Vector3.Distance(transform.position, targetPos) < 1)
        {
            moveLock = false;
        }

        poundAttackTickRate -= Time.deltaTime; 

        switch (currState)
        {
            case BOSSSTATES.REST:
                moveLock = true;
                break;

            case BOSSSTATES.SPRAY:

                break;

            case BOSSSTATES.LAZER:

                break;

            case BOSSSTATES.POUND:
                if (poundAttackTickRate < 0)
                {
                    var hitres = Physics2D.CircleCast(transform.position, 2.7f, new Vector2(0, 0));
                    hitres.transform.GetComponent<Lives>().Damage();
                    poundAttackTickRate = 0.3f;
                }
                break;
        }
    }

    private void initState(BOSSSTATES newState)
    {
        switch (currState)
        {
            case BOSSSTATES.REST:
                targetPos = transform.parent.position + new Vector3(0, -7, 0);
                moveLock = true;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                SpriteRend.sprite = Rest;
                break;

            case BOSSSTATES.SPRAY:
                Spray.toSpray = 10;
                targetPos = blackboard.target.position + new Vector3(0, 5, 0);
                moveLock = true;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                break;

            case BOSSSTATES.LAZER:
                targetPos = transform.parent.position;
                moveLock = true;
                Emerald.SetActive(true);
                Lazer.target = blackboard.target;
                Emerald.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;

            case BOSSSTATES.POUND:
                targetPos = blackboard.target.position + new Vector3(0, 10, 0);
                moveLock = true;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
        }
        StartCoroutine(StateTimer());
    }
    
    
    private void changeState(BOSSSTATES newState)
    {
        switch (currState)
        {
            case BOSSSTATES.REST:
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                SpriteRend.sprite = Angry;
                break;

            case BOSSSTATES.SPRAY:
                break;

            case BOSSSTATES.LAZER:
                Emerald.SetActive(false);
                break;

            case BOSSSTATES.POUND:
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
        }

        currState = newState;
        initState(newState);
        Debug.Log(currState);
    }

    private System.Collections.IEnumerator StateTimer()
    {
        yield return new WaitForSeconds(3);

        int newState = Random.Range(0, 4);

        switch (newState)
        {
            case 0:
                changeState(BOSSSTATES.POUND); break;
            case 1:
                changeState(BOSSSTATES.LAZER); break;
            case 2:
                changeState(BOSSSTATES.SPRAY); break;
            case 3:
                changeState(BOSSSTATES.REST); break;
        }
    }
}
