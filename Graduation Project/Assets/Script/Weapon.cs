using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator ani;
    [SerializeField]
    protected ParticleSystem flash;
    protected AudioClip fireSound;

    protected string weaponName;
    protected float damage;
    protected float range;    

    void Start()
    {
        //Ani = GetComponent<Animator>();
    }
}
