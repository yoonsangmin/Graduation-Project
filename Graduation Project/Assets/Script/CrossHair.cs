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

    public void SetCrouchAnimation(bool state) { ani.SetBool("IsCrouch", state); }
    public void SetWalkAnimation(bool state) { ani.SetBool("IsWalk", state); }
    public void SetRunAnimation(bool state) { ani.SetBool("IsRun", state); }

    public void StartFireAnimation() { ani.SetBool("IsFire", true); }
    public void StopFireAnimation() { ani.SetBool("IsFire", false); }
}
