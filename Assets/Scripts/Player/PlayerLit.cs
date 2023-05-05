using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLit : MonoBehaviour
{
    public PlayerInputControlls inputControll;
    public Vector2 inputDirection;

    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private PhysicsCheck pc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PhysicsCheck>();

        inputControll = new PlayerInputControlls();
        //ÌøÔ¾
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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (pc.isGround)
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

}
