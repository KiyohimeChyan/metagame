using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody2D rbody;

    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public Vector2 topOffset;
    [HideInInspector]
    public bool isGround;

    public bool isPlayer;

    public float checkRadius;
    public LayerMask groundLayer;

    [HideInInspector]
    public bool touchLeftWall;
    [HideInInspector]
    public bool touchRightWall;

    public bool touchTopWall;

    public bool isWall;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        if (isPlayer)
            playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        Check();
    }

    public void Check()
    {
        //ºÏ≤‚µÿ√Ê
        if (isWall)
        {
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRadius, groundLayer);
        }
        else
        {
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, 0), checkRadius, groundLayer);

        }

        //«ΩÃÂ≈–∂œ
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(leftOffset.x, leftOffset.y), checkRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(rightOffset.x, rightOffset.y), checkRadius, groundLayer);
        touchTopWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(topOffset.x, topOffset.y), checkRadius, groundLayer);

        if (isPlayer)
            isWall = (touchLeftWall && playerController.inputDirection.x < 0 || touchRightWall && playerController.inputDirection.x > 0) && rbody.velocity.y < 0;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(leftOffset.x, leftOffset.y), checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(rightOffset.x, rightOffset.y), checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(topOffset.x, topOffset.y), checkRadius);
    }
}
