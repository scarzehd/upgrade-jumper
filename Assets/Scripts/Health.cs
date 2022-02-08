using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public UnityAction onDeath;

    public UnityAction onDamage;

    public void Damage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                onDeath.Invoke();
            }
            else
            {
                onDamage.Invoke();
            }
        }
    }
}
