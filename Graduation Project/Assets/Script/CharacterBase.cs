using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject mainCamera;

    protected Rigidbody rb;
    protected CapsuleCollider col;
    [SerializeField]
    protected ParticleSystem bloodSpit;
    [SerializeField]
    protected Slider hpBar;

    protected float curLife;
    protected float maxLife;
    protected float walkSpeed;

    protected bool haveDamaged = false;

    protected void SetCharacterStat(float maxLife, float walkSpeed)
    {
        this.maxLife = maxLife;
        this.curLife = maxLife;
        hpBar.value = curLife / maxLife;
        this.walkSpeed = walkSpeed;
    }

    protected void HpBarLookAtCamera()
    {
        hpBar.transform.LookAt(mainCamera.transform);
    }

    protected void HpReset()
    {
        curLife = maxLife;
        hpBar.value = curLife / maxLife;
    }

    public void ReceiveDamage(float damage, Vector3 damagePos)
    {
        bloodSpit.transform.position = damagePos;
        bloodSpit.transform.rotation = Quaternion.Euler(0, Random.Range(-90.0f, 90.0f), 90.0f);
        bloodSpit.Play();

        ReceiveDamage(damage);
    }

    public void ReceiveDamage(float damage)
    {
        curLife -= damage;
        hpBar.value = curLife / maxLife;

        haveDamaged = true;
        if (IsInvoking("HaveDamaged") == true) CancelInvoke("HaveDamaged");
        else Invoke("HaveDamaged", 0.5f);
    }

    void HaveDamaged()
    {
        haveDamaged = false;
    }

    void Dead()
    {

    }
}
