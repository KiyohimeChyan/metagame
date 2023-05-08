using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerLit : MonoBehaviour
{
    public PlayerInputControlls inputControll;
    public Vector2 inputDirection;

    public float moveSpeed;
    public float jumpForce;

    public int currentHP;
    bool isDead;

    private Rigidbody2D rb;
    private PhysicsCheck pc;

    [Header("UIœ‡πÿ")]
    public TMP_Text hpText;
    public GameObject gameOverPanel;
    public Button selectedButton;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PhysicsCheck>();

        inputControll = new PlayerInputControlls();
        //Ã¯‘æ
        inputControll.Gameplay.Jump.started += Jump;
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
        inputDirection = inputControll.Gameplay.Move.ReadValue<Vector2>();
        hpText.text = "HP:" + currentHP;
        if (currentHP <= 0)
        {
            isDead = true;
            gameOverPanel.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        if(!isDead)
            Move();

    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (pc.isGround&&!isDead)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

        }
    }
    private void Move()
    {
        //int faceDir = (int)transform.localScale.x;
        //if (inputDirection.x > 0)
        //    faceDir = 1;
        //if (inputDirection.x < 0)
        //    faceDir = -1;
        //transform.localScale = new Vector3(faceDir, 1, 1);
        rb.velocity = new Vector2(inputDirection.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHP = Mathf.Max(currentHP - 1, 0);
        }
        else if (collision.gameObject.CompareTag("Boundary"))
        {
            currentHP = 0;
        }
        if(currentHP <= 0)
        {
            selectedButton.Select();

        }
    }

}
