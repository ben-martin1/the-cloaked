using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    bool collided = false;
    float damage;

    void Start()
    {
        damage = GetComponentInParent<BirdAI>().damage;
        Physics.IgnoreLayerCollision(9, 8);
        Physics.IgnoreLayerCollision(9, 6);
        Physics.IgnoreLayerCollision(9, 9);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Damageable damageable = collision.transform.GetComponent<Damageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
        collided = true;
        Destroy(gameObject);
    }

}
