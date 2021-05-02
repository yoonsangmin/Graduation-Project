using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageCol : MonoBehaviour
{
    [SerializeField] private BossEnemy boss = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<CharacterBase>().ReceiveDamage(boss._dashDamage);
        }
    }
}
