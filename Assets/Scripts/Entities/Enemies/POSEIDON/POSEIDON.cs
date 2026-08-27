using Unity.Mathematics;
using UnityEngine;

public class POSEIDON: MonoBehaviour
{
    enum BOSSSTATES
    {
        WALK,
        TP,
        LUNGE,
        THROW
    }

    [SerializeField] private BOSSSTATES currState = 0;
    public EnemyBlackboard blackboard;
    public Rigidbody2D rb;
    public SpriteRenderer SpriteRend;
    public Vector3 targetPos;
    public GameObject attackPoint;
    public float poundAttackTickRate = -1, chargeDist = 20;
    public bool moveLock;
    public GameObject miniProjectile;
    float ChargingProg;
    Vector2 StartingChPos, ChargingPos;

    //private Coroutine StateTimer;

    private void Start()
    {
        blackboard = gameObject.GetComponent<EnemyBlackboard>();
        SpriteRend = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        //changeState(BOSSSTATES.WALK);
    }

    private void Awake()
    {
        blackboard = gameObject.GetComponent<EnemyBlackboard>();
        SpriteRend = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        changeState(BOSSSTATES.WALK);
    }

    void Update()
    {
        poundAttackTickRate -= Time.deltaTime;

        if (moveLock)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 1);
        }

        if (Vector3.Distance(transform.position, targetPos) < 1)
        {
            moveLock = false;
        }

        switch (currState)
        {
            case BOSSSTATES.WALK:

                if (blackboard.target != null)
                {
                    rb.linearVelocityX = (blackboard.target.position - transform.position).normalized.x;
                }
                break;

            case BOSSSTATES.TP:
                if (!moveLock)
                {
                    changeState(BOSSSTATES.LUNGE);
                }
                break;

            case BOSSSTATES.LUNGE:


                if (ChargingProg < 1 && ChargingProg >= 0)
                {
                    transform.position = Vector2.Lerp(StartingChPos, ChargingPos, 1 - math.cos((ChargingProg * math.PI) / 2));
                    ChargingProg = math.clamp(ChargingProg + Time.deltaTime, 0, 1);
                }
                else if (ChargingProg < 0)
                {
                    blackboard.ChargeStartPosition = StartingChPos = transform.position;
                    blackboard.ChargeTargetPosition = ChargingPos = StartingChPos + (new Vector2(blackboard.target.position.x, blackboard.transform.position.y) - StartingChPos).normalized * chargeDist;
                    

                    RaycastHit2D hit2D = new RaycastHit2D();
                    if (hit2D = Physics2D.Raycast(StartingChPos, (ChargingPos - StartingChPos).normalized, chargeDist, LayerMask.GetMask("Ground")))
                    {
                        blackboard.ChargeTargetPosition = hit2D.point;
                    }
                    ChargingProg = 0;
                }
                else
                {
                    changeState(BOSSSTATES.WALK);
                }
                break;
                
            case BOSSSTATES.THROW:
                break;
        }
    }

    private void initState(BOSSSTATES newState)
    {
        switch (currState)
        {
            case BOSSSTATES.WALK:
                StartCoroutine(WalkTimer());
                if (blackboard.target != null)
                {
                rb.linearVelocityX = (blackboard.target.position - transform.position).normalized.x * 5;
                }
                break;
                
            case BOSSSTATES.TP:
                targetPos = blackboard.target.position + new Vector3(-blackboard.target.transform.localScale.x * 8, 0.5f, 0);
                moveLock = true;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                break;

            case BOSSSTATES.LUNGE:
                
                StartingChPos = blackboard.ChargeStartPosition;
                ChargingPos = blackboard.ChargeTargetPosition;
                StartCoroutine(AttackTimer());

                transform.localScale = new Vector3(rb.linearVelocity.normalized.x, 1, 1);
                break;

            case BOSSSTATES.THROW:

                var projectilePrefab = blackboard.projectile;
                if (projectilePrefab == null) return;

                var newProjectile = Instantiate(projectilePrefab);
                ProjectileController projectileController = newProjectile.GetComponent<ProjectileController>();
                if (projectileController != null) projectileController.damageAmount = blackboard.enemyData.baseDamage;

                newProjectile.transform.position = transform.position;

                newProjectile.transform.rotation = Quaternion.identity;
                Vector2 targetDirection = Vector2.zero;

                targetDirection = (Vector2)(blackboard.target.position - transform.position).normalized;

                Rigidbody2D rbp = newProjectile.GetComponent<Rigidbody2D>();
                rbp.AddForce(targetDirection * 10, ForceMode2D.Impulse);

                StartCoroutine(AttackTimer());
                break;
        }
    }

    private void changeState(BOSSSTATES newState)
    {
        switch (currState)
        {
            case BOSSSTATES.WALK:
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;

            case BOSSSTATES.TP:
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;

            case BOSSSTATES.LUNGE:
                ChargingProg = -1;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;

            case BOSSSTATES.THROW:
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
        }

        currState = newState;
        initState(newState);
        Debug.Log(currState);
    }

    private System.Collections.IEnumerator WalkTimer()
    {
        yield return new WaitForSeconds(2);

        int newState = UnityEngine.Random.Range(0, 2);
        switch (newState)
        {
            case 0:
                changeState(BOSSSTATES.TP); break;
            case 1:
                changeState(BOSSSTATES.THROW); break;
        }
    }

    private System.Collections.IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(2);

        changeState(BOSSSTATES.WALK);
        
    }
}
