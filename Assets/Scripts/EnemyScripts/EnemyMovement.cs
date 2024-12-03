using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum EnemyState
{
    Idle,
    Patroling,
    Knockback,
}
public class EnemyMovement : MonoBehaviour
{   

    [SerializeField] private float speed;
    [SerializeField] private Transform detectionPoint;
    [SerializeField] private float obstacleDetectRange;
    [SerializeField] private LayerMask patrolLayer;

    private EnemyState enemyState;
    private int facingDirection = -1;

    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (enemyState == EnemyState.Idle)
        {
            ChangeState(EnemyState.Patroling);
            Walk();
        }
        else if (enemyState == EnemyState.Patroling)
        {
            Patrol();
        }
        else if (enemyState == EnemyState.Knockback)
        {
            if (Mathf.Abs(rb.velocity.x) < 0.01f)
            {
                ChangeState(EnemyState.Patroling);
            }
        }

    }

    public void StartKnockback(Vector2 velocity)
    {
        ChangeState(EnemyState.Knockback);
        rb.velocity = velocity;
    }

    private void Patrol()
    {
        if (DirectionChangeCheck())
        {
            Flip();
        }
        Walk();
    }

    private void Walk()
    {
        rb.velocity = new Vector2(facingDirection * speed, rb.velocity.y);
    }

    private bool DirectionChangeCheck()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, obstacleDetectRange, patrolLayer);
        return hits.Length > 0;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void ChangeState(EnemyState newState)
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                anim.SetBool("IsIdle", false);
                break;

            case EnemyState.Patroling:
                anim.SetBool("IsPatroling", false);
                break;

            case EnemyState.Knockback:
                anim.SetBool("IsKnockingback", false);
                break;
        }

        enemyState = newState;

        switch (enemyState)
        {
            case EnemyState.Idle:
                anim.SetBool("IsIdle", true);
                break;

            case EnemyState.Patroling:
                anim.SetBool("IsPatroling", true);
                break;

            case EnemyState.Knockback:
                anim.SetBool("IsKnockingback", true);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, obstacleDetectRange);
    }
}
