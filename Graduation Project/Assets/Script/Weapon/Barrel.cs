using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    float explosionRange = 2.0f;
    bool isExplosion = false;

    [SerializeField]
    ParticleSystem explosion;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
            Explosion();            
    }

    void Explosion()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        
        //폭발 범위 내 데미지 주기
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
                col.gameObject.GetComponent<CharacterBase>().ReceiveDamage(50);
        }

        explosion.Play();
        isExplosion = true;
    }

    public void ReCreate()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;

        isExplosion = false;
    }

    public bool IsExplosion() { return isExplosion; }
}
