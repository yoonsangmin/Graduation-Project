using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]    protected ParticleSystem flash;
    protected AudioClip attackSound;

    protected string weaponName;
    public string _weaponName { get { return weaponName; } }
    protected float damage;
    protected float range;    
}
