using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterBase
{
    private NavMeshAgent enemyAi = null;
    public NavMeshAgent _enemyAi { get { return enemyAi; } }
    private List<Rigidbody> rigidBodys = new List<Rigidbody>();

    [SerializeField] private ParticleSystem summonParticle = null;
    [SerializeField] private BulletItem dropItem = null;

    void Start()
    {
        enemyAi = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();        
        col = GetComponent<CapsuleCollider>();
        enemyAi.speed = stat._walkSpeed;
        enemyAi.isStopped = false;

        dropItem.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        AnimatorSetting();
        HpBarLookAtCamera();
    }

    protected virtual void AnimatorSetting()
    {
        ani.SetFloat("Horizontal", enemyAi.velocity.normalized.x);
        ani.SetFloat("Vertical", -enemyAi.velocity.normalized.z);
        ani.SetBool("HaveDamaged", haveDamagedByBarrel || haveDamagedByBullet);
    }

    private void DieToVanish() { gameObject.SetActive(false); }

    override protected void Dead()
    {        
        GetComponent<BehaviorExecutor>().enabled = false;
        RandomDropItem();

        base.Dead();

        enemyAi.enabled = false;
        hpBar.gameObject.SetActive(false);

        foreach (Rigidbody rb in rigidBodys)
            rb.isKinematic = false;
        ani.enabled = false;
        
        Invoke("DieToVanish", 5.0f);
    }

    public void AddRigidBody(Rigidbody rb) { rigidBodys.Add(rb); }
    public void SummonParticlePlay() { summonParticle.Play(); }

    private void RandomDropItem()
    {
        int randomVal = Random.Range(0, 100);

        if (randomVal > 70)
        {
            dropItem.gameObject.SetActive(true);
            dropItem.SetDropBulletsNum(Random.Range(5, 20));
        }
    }
}