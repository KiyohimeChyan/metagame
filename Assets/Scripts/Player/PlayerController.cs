using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool isLevel2;

    //新输入系统
    public PlayerInputControlls inputControll;
    public Vector2 inputDirection;

    //移动相关
    public float moveSpeed;
    public float jumpForce;
    public float slideSpeed;
    public int slidePowerCost;
    public Vector2 wallJumpForce;
    public float slideDistance;
    private Rigidbody2D rb;
    private PhysicsCheck pc;
    bool isCrouch;
    bool isWallJump;
    bool isSlide;
    private CapsuleCollider2D coll;
    Vector2 originOffset;
    Vector3 originSize;

    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    //战斗相关
    private CharacterStats characterStats;
    private PlayerStats playerStats;
    public float hurtForce;
    bool isHurt;
    bool isDead;
    bool isStuck;
    public bool isAttack;
    public bool isReading;

    //动画相关
    private Animator anim;

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pc = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        characterStats = GetComponent<CharacterStats>();
        playerStats = GetComponent<PlayerStats>();

        originOffset = coll.offset;
        originSize = coll.size;

        inputControll = new PlayerInputControlls();
        //跳跃
        inputControll.Gameplay.Jump.started += Jump;
        //攻击
        inputControll.Gameplay.Attack.started += PlayerAttack;
        //滑铲
        if (!isLevel2)
        {
            inputControll.Gameplay.Slide.started += PlayerSlide;
        }
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
        if (!isReading)
        {
            CheckState();
            AnimControll();
            isStuck = pc.touchTopWall && !isSlide && pc.isGround;
            inputDirection = inputControll.Gameplay.Move.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        if(!isHurt&&!isAttack&&!isReading)
            Move();

    }

    private void Move()
    {
        if (!isCrouch&&!isWallJump && !isStuck)
            rb.velocity = new Vector2(inputDirection.x * playerStats.MoveSpeed * Time.fixedDeltaTime, rb.velocity.y);
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

        if (isSlide)
        {
            coll.offset = new Vector2(-0.06f, 0.4f);
            coll.size = new Vector2(0.7f, 0.8f);
        }
        else
        {
            coll.offset = originOffset;
            coll.size = originSize;
        }

    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (!isReading)
        {
            if (pc.isGround)
            {
                rb.AddForce(transform.up * playerStats.JumpForce, ForceMode2D.Impulse);
                //StopAllCoroutines();  //跳跃打断滑铲
                //isSlide = false;

            }
            else if (pc.isWall)
            {
                if (!isLevel2)
                {
                    rb.AddForce(new Vector2(-transform.localScale.x, wallJumpForce.y) * wallJumpForce.x, ForceMode2D.Impulse);
                    isWallJump = true;
                }
            }
        }
    }

    private void PlayerSlide(InputAction.CallbackContext obj)
    {
        if (!isSlide&&pc.isGround&&characterStats.CurrentPower>=slidePowerCost&&!isReading)
        {
            isSlide = true;
            var targetPos = new Vector3(transform.position.x + playerStats.SlideDistance * transform.localScale.x, transform.position.y);
            var targetPos1 = new Vector3(transform.position.x - playerStats.SlideDistance * transform.localScale.x, transform.position.y);

            StartCoroutine(TriggerSlide(targetPos,targetPos1));

            characterStats.OnSlide(slidePowerCost);
        }
    }

    IEnumerator TriggerSlide(Vector3 target, Vector3 targetReverse)
    {
        do
        {
            yield return null;

            if ((pc.touchLeftWall&&transform.localScale.x<0) || (pc.touchRightWall && transform.localScale.x > 0) || !pc.isGround)
            {
                break;
            }
            rb.MovePosition(new Vector2(transform.position.x + transform.localScale.x * slideSpeed, transform.position.y));


        } while (Mathf.Abs(transform.position.x - target.x) > 0.3f&& Mathf.Abs(transform.position.x - targetReverse.x) > 0.31f);
        isSlide = false;
    }


    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        if (!pc.isWall)
        {
            PlayAttack();
            isAttack = true;
        }
    }

    private void AnimControll()
    {
        anim.SetFloat("moveSpeed",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("JumpSpeed", rb.velocity.y);
        anim.SetBool("isGround", pc.isGround);
        anim.SetBool("isCrouch", isCrouch);
        anim.SetBool("isDead", isDead);
        anim.SetBool("isAttack", isAttack);
        if (!isLevel2)
        {
            anim.SetBool("isWall", pc.isWall);
            anim.SetBool("isSlide", isSlide);
            anim.SetBool("isStuck", isStuck);
        }
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
        if (isDead)//slide是否要无敌
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");

        coll.sharedMaterial = pc.isGround ? normal : wall;
        if (pc.isWall)
        {
            if (!isLevel2)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2f);
            }
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y);
        }
        if (isWallJump && rb.velocity.y < 0)
        {
            isWallJump = false;
        }

    }
    #endregion

}
