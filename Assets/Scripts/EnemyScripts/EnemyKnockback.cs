using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private EnemyMovement enemyMovement;

    public void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Knockback(Transform forceTransform, float knockbackForce)
    {
        Vector2 direction = (transform.position - forceTransform.position).normalized;
        Vector2 velocity = direction * knockbackForce;
        enemyMovement.StartKnockback(velocity);
    }
}
