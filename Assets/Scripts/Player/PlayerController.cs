using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //新输入系统
    public PlayerInputControlls inputControll;
    public Vector2 inputDirection;

    //移动相关
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    private PhysicsCheck pc;
    bool isCrouch;
    private CapsuleCollider2D coll;
    Vector2 originOffset;
    Vector3 originSize;

    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    //战斗相关
    private CharacterStats characterStats;
    public float hurtForce;
    bool isHurt;
    bool isDead;
    public bool isAttack;

    //动画相关
    private Animator anim;

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        pc = GetComponent<PhysicsCheck>();
        characterStats = GetComponent<CharacterStats>();

        originOffset = coll.offset;
        originSize = coll.size;

        inputControll = new PlayerInputControlls();
        //跳跃
        inputControll.Gameplay.Jump.started += Jump;
        //攻击
        inputControll.Gameplay.Attack.started += PlayerAttack;
    }

    private void OnEnable()
    {
        inputControll.Enable();
    }

    private void OnDisable()
    {
        inputControll.Disable();
    }

    private void Update()
    {
        CheckState();
        AnimControll();
        inputDirection = inputControll.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(!isHurt&&!isAttack)
            Move();

    }

    private void Move()
    {
        if (!isCrouch)
            rb.velocity = new Vector2(inputDirection.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        transform.localScale = new Vector3(faceDir, 1, 1);
        isCrouch = inputDirection.y < -0.5f && pc.isGround;
        if (isCrouch)
        {
            coll.offset = new Vector2(-0.06f, 0.6f);
            coll.size = new Vector2(0.7f, 1.2f);
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            coll.offset = originOffset;
            coll.size = originSize;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (pc.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

        }
    }

    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        PlayAttack();
        isAttack = true;
    }

    private void AnimControll()
    {
        anim.SetFloat("moveSpeed",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("JumpSpeed", rb.velocity.y);
        anim.SetBool("isGround", pc.isGround);
        anim.SetBool("isCrouch", isCrouch);
        anim.SetBool("isDead", isDead);
        anim.SetBool("isAttack", isAttack);
    }
    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }

    private void PlayAttack()
    {
        anim.SetTrigger("attack");
    }

    #region 受伤相关

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

    }
    public void StopHurt()
    {
        isHurt = false;
    }
    #endregion

    #region 死亡相关
    public void Dead()
    {
        isDead = true;
        inputControll.Gameplay.Disable();
    }
    private void CheckState()
    {
        if (isDead)
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");

        coll.sharedMaterial = pc.isGround ? normal : wall;
    }
    #endregion

}
