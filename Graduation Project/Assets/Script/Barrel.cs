using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField]
    ParticleSystem explosion;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Explosion();            
        }
    }

    void Explosion()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
        explosion.Play();
        explosion.GetComponent<ExplosionArea>().enabled = true;
    }
}
