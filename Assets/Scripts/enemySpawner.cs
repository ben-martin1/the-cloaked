using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float timer;
    public int maxSpawned;
    private GameObject spawnedEnemy;
    public int currentSpawned;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Respawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Respawner()
    {
        for (currentSpawned = 0; currentSpawned <= maxSpawned; currentSpawned++)
        {
            spawnedEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timer);
        }
    }

}
