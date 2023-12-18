using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeAI : Damageable
{
   float targetY;
   public float respawnTimer;
    private float timeToRespawn;
    private bool countDown = false;
    Damageable damageable;

    void Start()
    {
        targetY = transform.position.y;
        damageable = GetComponent<Damageable>();
    }
    void Update()
    {
        if (!isDead)
        {
           // transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
        if (countDown)
        {
            timeToRespawn -= Time.deltaTime;
            if (timeToRespawn < .3)
            {
                respawn();
            }
        }
    }
    public override void Die()
    {
        countDown = true;
        transform.position = new Vector3(transform.position.x, targetY-10, transform.position.z);
        timeToRespawn = respawnTimer;
    }
    void respawn()
    {
        countDown = false;
        damageable.isDead = false;
        damageable.setHealth(damageable.maxHealth);
    }
}
