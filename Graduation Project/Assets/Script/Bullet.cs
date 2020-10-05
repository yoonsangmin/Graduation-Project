using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    ParticleSystem Trace;
    [SerializeField]
    GameObject Shape;

    Rigidbody Rb;
    Collider Col;

    Vector3 Start_Pos;

    //총알의 변수
    float accuracy;
    float range;
    float speed;
    float damage;

    //총알의 상태 변수
    bool is_fired = false;
    bool is_col = false;

    public void SetBullet(float accuracy, float range, float speed, float damage)
    {
        this.accuracy = accuracy;
        this.range = range;
        this.speed = speed;
        this.damage = damage;
    }

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();        
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(Start_Pos, transform.position) >= range)
            Vanish();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet") return;

        Invoke("Vanish", 2.0f);
        Rb.velocity = new Vector3(0, 0, 0);
        Col.enabled = false;
        Shape.SetActive(false);
        
        //이펙트        
        Trace.transform.position = collision.contacts[0].point;
        Trace.Play();
    }

    void Vanish()
    {
        this.gameObject.SetActive(false);
    }

    public float GetDamage()
    {
        return damage;
    }

    public void Fire()
    {
        Shape.SetActive(true);
        Col.enabled = true;
        Start_Pos = transform.position;
        Rb.velocity = (transform.forward + new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), 0)) * speed;
    }
}
