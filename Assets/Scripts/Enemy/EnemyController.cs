using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public PhysicsCheck physicsCheck;

    [Header("移动相关")]
    public float normalSpeed;
    public float chaseSpeed;
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public Vector3 faceDir;

    //等待计时器
    [HideInInspector]
    public bool isWait;
    public float waitTimer;
    float waitTime;

    public float lostTimer;
    [HideInInspector]
    public float lostTime;

    [Header("追击检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    //受伤
    Transform attacker;
    bool isHurt;
    public float hurtForce;

    bool isDead;

    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        currentState.LogicUpdate();
        PatrolWait();
    }

    private void FixedUpdate()
    {
        if (!isHurt&&!isDead&&!isWait)
            EnemyMove();
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public virtual void EnemyMove()
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void PatrolWait()
    {
        if (isWait)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {                
                isWait = false;
                waitTime = waitTimer;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
        if (!FoundPlayer() && lostTime > 0)
        {
            lostTime -= Time.deltaTime;
        }
        else if (FoundPlayer())    // 添加这个额外的判断，在发现玩家的时候重置丢失时间
        {
            lostTime = lostTimer;
        }
    }

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
    }

    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public void OnEnemyTakeDamage(Transform attackerTrans)
    {
        attacker = attackerTrans;
        if (transform.position.x < attacker.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (transform.position.x > attacker.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(OnEnemyHurt(dir));
    }

    IEnumerator OnEnemyHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnEnemyDie()
    {
        gameObject.layer = 2;
        isDead = true;
        anim.SetBool("isDead", isDead);
    }

    public void DestroyAfterDeadAnim()
    {
        Destroy(this.gameObject);
    }

}
