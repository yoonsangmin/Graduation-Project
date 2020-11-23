using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    Animator ani;
    [SerializeField]
    Image zoomMode = null;
    [SerializeField]
    GameObject originMode = null;

    void Start()
    {
        ani = originMode.GetComponent<Animator>();
        zoomMode.gameObject.SetActive(false);
    }

    public void SetCrouchAnimation(bool state) { ani.SetBool("IsCrouch", state); }
    public void SetWalkAnimation(bool state) { ani.SetBool("IsWalk", state); }
    public void SetRunAnimation(bool state) { ani.SetBool("IsRun", state); }

    public void StartFireAnimation() { ani.SetBool("IsFire", true); }
    public void StopFireAnimation() { ani.SetBool("IsFire", false); }

    public void ActiveZoomMode()
    {
        zoomMode.gameObject.SetActive(true);
        originMode.SetActive(false);
    }
    public void DeactiveZoomMode()
    {
        zoomMode.gameObject.SetActive(false);
        originMode.SetActive(true);
    }
}
