using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    float health;
    public float maxHealth;
    public bool isDead = false;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("Taking Damage");
        health -= amount;
        if (health <= 0f)
        {
            isDead = true;
            Die();
        }
    }
    public void setHealth(float amount)
    {
        health = amount;
    }

    public virtual void Die()
    {
        Debug.Log("dead");
        //override this method to modify ondeath event
        Destroy(gameObject);
    }
}
