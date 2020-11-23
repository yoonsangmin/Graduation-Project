using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem flash;
    protected AudioClip attackSound;

    protected string weaponName;
    protected float damage;
    protected float range;    

}
