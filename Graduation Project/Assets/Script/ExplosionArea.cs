using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionArea : MonoBehaviour
{
    private void Start()
    {
        enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
            other.gameObject.GetComponent<CharacterBase>().ReceiveDamage(50);
        enabled = false;
    }  
}
