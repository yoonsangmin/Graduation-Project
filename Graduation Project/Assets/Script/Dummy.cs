using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : CharacterBase
{
    [SerializeField]
    GameObject shape;
    float revivalTime = 5.0f; 

    void Start()
    {     
        SetCharacterStat(100.0f, 0.0f);
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (curLife <= 0)
            StartCoroutine(RevivalCoroutine());
    }

    IEnumerator RevivalCoroutine()
    {
        shape.SetActive(false);
        col.enabled = false;
        yield return new WaitForSeconds(revivalTime);

        curLife = maxLife;
        shape.SetActive(true);
        col.enabled = true;
    }
}
