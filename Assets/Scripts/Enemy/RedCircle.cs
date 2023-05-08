using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCircle : MonoBehaviour
{
    public float patrolTime;
    public float moveSpeed;
    float patrolTimer;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (patrolTimer >= 0)
        {
            patrolTimer -= Time.deltaTime;
        }
        else
        {
            patrolTimer = patrolTime;
            var currentX = transform.localScale.x;
            transform.localScale = new Vector3(-currentX, 1, 1);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(transform.localScale.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);

    }
}
