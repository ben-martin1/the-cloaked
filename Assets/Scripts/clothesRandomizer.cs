using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clothesRandomizer : MonoBehaviour
{
    public int maxNum;

    public int num1;
    public int num2;

    public GameObject GO1;
    public GameObject GO2;
    public GameObject GO3;
    public GameObject GO4;

    SkinnedMeshRenderer skinnedMeshRenderer1;
    SkinnedMeshRenderer skinnedMeshRenderer2;
    SkinnedMeshRenderer skinnedMeshRenderer3;
    SkinnedMeshRenderer skinnedMeshRenderer4;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer1 = GO1.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer2 = GO2.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer3 = GO3.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer4 = GO4.GetComponent<SkinnedMeshRenderer>();

       // GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        num1 = Random.Range(1, maxNum);
        num2 = Random.Range(1, maxNum);

        if(num1 == 1f)
        {
            skinnedMeshRenderer1.enabled = true;
            skinnedMeshRenderer2.enabled = false;
            skinnedMeshRenderer1.material.color = Random.ColorHSV(0f, .1f, .1f, .2f, 0.5f, 1f);
        }    
        if(num1 == 2f)
        {
            skinnedMeshRenderer1.enabled = false;
            skinnedMeshRenderer2.enabled = true;
            skinnedMeshRenderer2.material.color = Random.ColorHSV(0f, .1f, .1f, .2f, 0.5f, 1f);
        }     
        if(num2 == 1f)
        {
            skinnedMeshRenderer3.enabled = true;
            skinnedMeshRenderer4.enabled = false;
            skinnedMeshRenderer3.material.color = Random.ColorHSV(0f, .1f, .1f, .2f, 0.5f, 1f);
        }     
        if(num2 == 2f)
        {
            skinnedMeshRenderer3.enabled = false;
            skinnedMeshRenderer4.enabled = true;
            skinnedMeshRenderer4.material.color = Random.ColorHSV(0f, .1f, .1f, .2f, 0.5f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
