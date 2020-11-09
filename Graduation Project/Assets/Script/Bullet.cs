using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    ParticleSystem trace = null;
    [SerializeField]
    GameObject shape = null;

    Rigidbody rb;
    Collider col;

    Vector3 startPos;

    //총알의 변수
    float accuracy;
    float range;
    float speed;
    float damage;

    //총알의 상태 변수
    bool isFired = false;
    bool isCol = false;

    public void SetBullet(float accuracy, float range, float speed, float damage)
    {
        this.accuracy = accuracy;
        this.range = range;
        this.speed = speed;
        this.damage = damage;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(startPos, transform.position) >= range)
            Vanish();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet") return;

        transform.parent = collision.gameObject.transform;

        Invoke("Vanish", 1.0f);
        rb.velocity = new Vector3(0, 0, 0);
        col.enabled = false;
        shape.SetActive(false);

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") collision.gameObject.GetComponent<CharacterBase>().ReceiveDamage(damage, collision.contacts[0].point);
        else
        { 
            //이펙트        
            trace.transform.position = collision.contacts[0].point;
            trace.Play();
        }
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
        shape.SetActive(true);
        col.enabled = true;
        startPos = transform.position;
        rb.velocity = (transform.forward + new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), 0)) * speed;
    }
}
