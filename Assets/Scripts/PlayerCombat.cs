using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;

    //TODO: move these fields to a stats manager later
    [SerializeField] private float weaponRange;
    [SerializeField] private float knockbackForce;
    [SerializeField] private int damage;

    public void Attack()
    {
        anim.SetBool("IsAttacking", true);
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            if (!enemies[0].GetComponent<EnemyHealth>().Damage(damage))
            {
                enemies[0].GetComponent<EnemyMovement>().Kill();
                return; //return early because we don't want to knock a dead enemy back
            }
            //knockback here
            EnemyKnockback enemyKnockback = enemies[0].GetComponent<EnemyKnockback>();
            if (enemyKnockback != null) //for things that cannot be knocked back like buildings
            {
                enemyKnockback.Knockback(transform, knockbackForce);
            }
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("IsAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 0.5f);
    }
}
