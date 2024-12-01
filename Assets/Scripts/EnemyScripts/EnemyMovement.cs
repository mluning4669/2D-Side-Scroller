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
    [SerializeField] private LayerMask wallLayer;

    private EnemyState enemyState;
    private int facingDirection = -1;

    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Patroling);
        rb.velocity = new Vector2(facingDirection * speed, rb.velocity.y);
    }

    // Update is called once per frame
    public void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            DirectionChangeCheck();
            Patrol();
        }

    }

    private void Patrol()
    {
        rb.velocity = new Vector2(facingDirection * speed, rb.velocity.y);
    }

    private void DirectionChangeCheck()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, obstacleDetectRange, wallLayer);

        if (hits.Length > 0)
        {
            Flip();
        }
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
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, obstacleDetectRange);
    }
}
