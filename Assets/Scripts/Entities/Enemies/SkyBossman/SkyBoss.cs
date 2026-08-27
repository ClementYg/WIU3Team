using UnityEngine;

public class SkyBoss : MonoBehaviour
{
    enum BOSSSTATES
    {
        PUSH,
        ROCK,
        SLAM
    }

    private BOSSSTATES currState = 0;
    public GameObject Rock;
    public EnemyBlackboard blackboard;
    //public Rigidbody2D rb;
    public SpriteRenderer SpriteRend;
    //public Sprite Angry, Rest;
    public Vector3 targetPos;
    public GameObject attackPoint;
    public float RockAttackTickRate = -1;
    public int RockNum;
    //private Coroutine StateTimer;

    private void Awake()
    {
        blackboard = gameObject.GetComponent<EnemyBlackboard>();
        SpriteRend = gameObject.GetComponent<SpriteRenderer>();
        //rb = gameObject.GetComponent<Rigidbody2D>();
        Random.InitState((int)System.DateTime.Now.Ticks);
        changeState(BOSSSTATES.PUSH);
    }

    void Update()
    {
        
        RockAttackTickRate -= Time.deltaTime; 

        switch (currState)
        {
            case BOSSSTATES.PUSH:
                break;

            case BOSSSTATES.ROCK:
                if (RockAttackTickRate < 0)
                {
                    Instantiate(Rock, transform.position + new Vector3(Random.Range(-12f,12f), 10,0), transform.rotation);
                    RockAttackTickRate = 0.5f;
                }
                break;

            case BOSSSTATES.SLAM:

                break;
        }
    }

    private void initState(BOSSSTATES newState)
    {
        switch (currState)
        {
            case BOSSSTATES.PUSH:
                
                break;

            case BOSSSTATES.ROCK:
                //Spray.toSpray = 10;
                break;

            case BOSSSTATES.SLAM:
                //Emerald.SetActive(true);
                //Lazer.target = blackboard.target;
                //Emerald.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
        }
        StartCoroutine(StateTimer());
    }
    
    
    private void changeState(BOSSSTATES newState)
    {
        switch (currState)
        {
            case BOSSSTATES.PUSH:
                //rb.constraints = RigidbodyConstraints2D.FreezeAll;
                //SpriteRend.sprite = Angry;
                break;

            case BOSSSTATES.ROCK:
                break;

            case BOSSSTATES.SLAM:
                //Emerald.SetActive(false);
                break;
        }

        currState = newState;
        initState(newState);
        Debug.Log(currState);
    }

    private System.Collections.IEnumerator StateTimer()
    {
        yield return new WaitForSeconds(5);

        switch (((int)currState))
        {
            case 0:
                changeState(BOSSSTATES.ROCK); break;
            case 1:
                changeState(BOSSSTATES.SLAM); break;
            case 2:
                changeState(BOSSSTATES.PUSH); break;
        }
    }
}
