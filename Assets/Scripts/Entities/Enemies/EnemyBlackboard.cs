using UnityEngine;
using System.Collections.Generic;

public class EnemyBlackboard : MonoBehaviour
{
    [HideInInspector] public bool isDead = false;

    [Header("References")]
    public GameObject projectile;
    public Transform target;
    public EntityData enemyData;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public bool isInvulnerable = false;
    [HideInInspector] public bool timerEnded = false;

    [Header("Movement Attributes")]
    public float maxMoveSpeed = 3f;
    public float moveForce = 15f;

    [Header("Waypoint Attributes")]
    public List<Vector3> waypoints;
    [HideInInspector] public int currentWaypointIndex;
    public int startingWaypointIndex;

    [Header("Attack Attributes")]
    public Transform attackPoint;
    public float attackCooldown = 1f;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    [HideInInspector] public float lastAttackTime;

    [Header("Sprite Attributes")]
    public bool spriteDefaultFacesRight = true;
    [HideInInspector] public bool animationFinished = false;

    [HideInInspector] public float chargeProgress;
    [HideInInspector] public Vector2 ChargeTargetPosition;
    [HideInInspector] public Vector2 ChargeStartPosition;
    [HideInInspector] public bool AtkAnimTrig = false;


    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }
    private void Awake()
    {
        ChargeTargetPosition = ChargeStartPosition = transform.position;

        currentWaypointIndex = startingWaypointIndex;
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnAnimationFinished()
    {
        animationFinished = true;
    }

    public void OnAttackTrigger()
    {
        AtkAnimTrig = true;
    }
}
