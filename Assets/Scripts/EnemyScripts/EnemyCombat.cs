using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float knockbackForce;
    [SerializeField] private int knockbackDamage;
    [SerializeField] private LayerMask enemyLayer;
    
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StrikeOtherEnemy(Collision2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<EnemyHealth>().Damage(knockbackDamage);

            if (collision.rigidbody.TryGetComponent<EnemyKnockback>(out var enemyKnockback)) //for things that cannot be knocked back like buildings
            {
                enemyKnockback.Knockback(transform, knockbackForce);
            }
        }
    }
}
