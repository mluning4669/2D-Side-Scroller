using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void Attack()
    {
        anim.SetBool("IsAttacking", true);
    }

    public void FinishAttacking()
    {
        anim.SetBool("IsAttacking", false);
    }
}
