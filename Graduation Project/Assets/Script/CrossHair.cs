using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    Animator Ani;

    void Start()
    {
        Ani = GetComponent<Animator>();
    }

    public void SetAnimation(Player Player)
    {
        Ani.SetBool("Crouch", Player.IsPlayerCrouch());
        Ani.SetBool("Walk", Player.IsPlayerWalk());
        Ani.SetBool("Run", Player.IsPlayerRun());
    }

    public void StartFireAnimation() { Ani.SetBool("Fire", true); }
    public void StopFireAnimation() { Ani.SetBool("Fire", false); }
}
