using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerEnemy : Enemy
{
    [SerializeField] private ParticleSystem explosion = null;
    [SerializeField] private SkinnedMeshRenderer shape = null;
    [SerializeField] private GameObject hitCol = null;
    [SerializeField] private float readyToBoomTime = 1.0f;    
    private float explosionRange = 6.0f;
    
    override protected void AnimatorSetting()
    {
        base.AnimatorSetting();
    }

    override protected void Die()
    {
        StartCoroutine(BoomCoroutine());
    }

    private IEnumerator BoomCoroutine()
    {
        enemyAi.enabled = false;
        ani.SetTrigger("BoomTrigger");

        yield return new WaitForSeconds(readyToBoomTime);
        hitCol.GetComponent<CapsuleCollider>().enabled = false;
        ani.enabled = false;        

        Boom();
        Invoke("DieToVanish", 5.0f);        
    }

    private void Boom()
    {
        //폭발 범위 내 데미지 주기
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<CharacterBase>().ReceiveDamage(50);
                col.gameObject.GetComponent<CharacterBase>().ExplosionAction(300.0f, gameObject.transform.position, explosionRange * 5.0f);
            }
        }
        shape.enabled = false;
        explosion.Play();
    }

    public void BoomAction()
    {
        Dead();
    }
}
