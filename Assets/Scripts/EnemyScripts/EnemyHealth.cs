using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public bool Damage(int amount)
    {
        return ChangeHealth(-amount);
    }

    public bool Heal(int amount)
    {
        return ChangeHealth(amount);
    }

    public bool ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        else if (currentHealth <= 0)
        {
            return false;
        }

        return true;
    }
}
