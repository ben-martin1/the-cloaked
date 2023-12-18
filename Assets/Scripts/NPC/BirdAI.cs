using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BirdAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject[] enemy;
    public float health;
    public float stopDistance;
    public LayerMask enemyLayer;

    public GameObject target;

    //attacking
    public Transform firePoint;
    bool alreadyAttacked;
    public float timeBetweenAttacks;
    public GameObject projectile;
    public float damage;
    //public Rigidbody rb;

    //states
    public float sightRange;
    public float stopFollowRange;
    public bool enemyInSightRange;
    public enum NPCStates
    {
        attack, follow,
    }
    public NPCStates NPCState;

    private void Awake()
    {
        player = GameObject.Find("Merlin").transform;
        agent = GetComponent<NavMeshAgent>();
        Physics.IgnoreLayerCollision(8, 6);
    }

    void Update()
    {
        manageState();
    }
    private void manageState()
    {
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, enemyLayer);

        if (enemyInSightRange)
        {
            NPCState = NPCStates.attack;
        }
        else NPCState = NPCStates.follow;
        switch (NPCState)
        {
            case NPCStates.attack: Attacking();
                break;
            case NPCStates.follow: Following(); 
                break;
        }
    }
    private void Attacking()
    {
        getTarget(enemyLayer);

        if (target != null)
        {
        agent.updateRotation = false;
            FaceTarget();
            if(stopDistance >= Vector3.Distance(transform.position, target.transform.position))
            {
                agent.SetDestination(transform.position);
            }else agent.SetDestination(target.transform.position);
        }
       if(!alreadyAttacked)
        {
            GameObject GO = Instantiate(projectile, firePoint.position, Quaternion.identity);
            //GO.AddComponent<ProjectileScript>();

            Rigidbody rb = GO.GetComponent<Rigidbody>();
            rb.gameObject.layer = 9;
            rb.MovePosition(target.transform.position);
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            rb.transform.parent = gameObject.transform;
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void getTarget(LayerMask targetLayer)
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, sightRange, targetLayer);
        float minimumDistance = sightRange;
        foreach (Collider enemy in nearbyEnemies)
        {
            float distance = Vector3.Distance(player.transform.position, enemy.gameObject.transform.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                target = enemy.gameObject;
            }
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Following()
    {
        if (stopDistance >= Vector3.Distance(transform.position, player.transform.position))
        {
            agent.SetDestination(transform.position);
        }
        else agent.SetDestination(player.transform.position);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(Die), .05f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); ;
        transform.rotation = lookRotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
