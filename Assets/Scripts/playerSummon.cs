using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSummon : MonoBehaviour
{
    public GameObject activeSummon;
    public int summonCount;
    public List<GameObject> summonedCreatures;
    public int summonMax;
    public float summonRange;
    Vector3 spawnPoint;

    //SFX
    public GameObject summonEffect;
    public GameObject noActive;
    public GameObject desummoned;

    void Start()
    {
        summonedCreatures = new List<GameObject>();
    }
    void Update()
    {
        Debug.Log(hasAvailableSummons());
    }

    public bool hasAvailableSummons()
    {
        if(summonCount >= summonMax)
        {
            return false;
        }
        if(activeSummon == null)
        {
            return false;
        }
        return true;
    }
    public void summonShot(Vector3 point, GameObject activeSummon)
    {
        Debug.Log("Starting Coroutine");
        StartCoroutine(summonLifeCycle(point, activeSummon));
    }
    public void updateActiveSummon(GameObject obj)
    {
        activeSummon = obj;
    }
    IEnumerator summonLifeCycle(Vector3 point, GameObject activeSummon)
    {

        float summonLife = activeSummon.GetComponentInChildren<Interactable>().summonLife;

        if (Vector3.Distance(point, transform.position) < summonRange)
        {
            spawnPoint = point;
        }
        else if ((Vector3.Distance(point, transform.position) >= summonRange))
        {
            spawnPoint = (transform.position - point).normalized;
        }
        GameObject summon = Instantiate(activeSummon, spawnPoint, Quaternion.identity);
        summon.tag = "Summoned";
        summon.layer = 8;
        GameObject summonSFX = Instantiate(summonEffect, spawnPoint, Quaternion.identity);
        Destroy(summonSFX, .8f);
        summonedCreatures.Add(summon);
        summonCount += 1;
        yield return new WaitForSeconds(summonLife);
        summonCount -= 1;
        summonedCreatures.Remove(summon);
        GameObject desummonedSFX = Instantiate(desummoned, summon.transform.position, Quaternion.identity);
        Destroy(desummonedSFX, 4);
        Destroy(summon);
        yield return null;
    }
}
