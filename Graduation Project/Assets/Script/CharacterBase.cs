using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Rigidbody rb;
    protected CapsuleCollider col;

    protected float curLife;
    protected float maxLife;
    protected float walkSpeed;

    protected void SetCharacterStat(float maxLife, float walkSpeed)
    {
        this.maxLife = maxLife;
        this.curLife = maxLife;
        this.walkSpeed = walkSpeed;
    }

    public void ReceiveDamage(float damage) { curLife -= damage; }
}
