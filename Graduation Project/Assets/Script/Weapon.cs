using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator Ani;
    [SerializeField]
    protected ParticleSystem Flash;
    protected AudioClip Fire_Sound;

    protected string weapon_name;
    protected float damage;
    protected float range;    

    void Start()
    {
        //Ani = GetComponent<Animator>();
    }
}
