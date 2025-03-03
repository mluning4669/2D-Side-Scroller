using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public enum EnemyState
{
    Idle,
    Patroling,
    Knockback,
    Dead,
}
public class EnemyMovement : MonoBehaviour
{   

    [SerializeField] private float speed;
    [SerializeField] private Transform detectionPoint;
    [SerializeField] private Transform groundDetector;
    [SerializeField] private float obstacleDetectRange;
    [SerializeField] private LayerMask patrolLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int facingDirection = -1;

    private EnemyState enemyState;

    private Rigidbody2D rb;
    private Animator anim;
    private EnemyCombat enemyCombat;
    private EnemyHealth enemyHealth;
    private EnemyAudio enemyAudio;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyCombat = GetComponent<EnemyCombat>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAudio = GetComponent<EnemyAudio>();
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
                if (enemyHealth.IsDead())
                {
                    Kill();
                }
                else
                {
                    ChangeState(EnemyState.Patroling);
                }
            }
        }

    }

    public void StartKnockback(Vector2 velocity)
    {
        ChangeState(EnemyState.Knockback);
        enemyAudio.PlayHitEfect();
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
        Collider2D[] patrolHits = Physics2D.OverlapCircleAll(detectionPoint.position, obstacleDetectRange, patrolLayer);
        Collider2D[] groundHits = Physics2D.OverlapCircleAll(groundDetector.position, obstacleDetectRange, groundLayer);
        return patrolHits.Length > 0 || groundHits.Length == 0;
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

            case EnemyState.Dead:
                anim.SetBool("IsDead", true);
                break;
        }
    }

    public void Kill()
    {
        ChangeState(EnemyState.Dead);
        enemyAudio.PlayDeathEffect();
        rb.velocity = Vector2.zero;
    }

    public void DropLoot()
    {
        LootManager.Instance.DropBigDiamond(gameObject.transform);
    }

    public void Destroy()
    { 
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyState == EnemyState.Knockback)
        {
            enemyCombat.StrikeOtherEnemy(collision);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, obstacleDetectRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundDetector.position, obstacleDetectRange);
    }
}
