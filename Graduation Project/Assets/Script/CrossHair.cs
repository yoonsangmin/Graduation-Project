using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void SetAnimation(Player player)
    {
        ani.SetBool("IsCrouch", player.IsPlayerCrouch());
        ani.SetBool("IsWalk", player.IsPlayerWalk());
        ani.SetBool("IsRun", player.IsPlayerRun());
    }

    public void StartFireAnimation() { ani.SetBool("IsFire", true); }
    public void StopFireAnimation() { ani.SetBool("IsFire", false); }
}
