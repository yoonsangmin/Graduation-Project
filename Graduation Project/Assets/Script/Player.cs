using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    [SerializeField]
    CrossHair crossHair;
    [SerializeField]
    PlayerWeaponController weaponController;
    [SerializeField]
    GameObject arm;

    //컴포넌트
    Animator ani;

    //이동
    float curSpeed;
    float runSpeed;
    float crouchSpeed;
    bool isRun = false;
    bool isWalk = false;

    //점프
    float jumpForce = 5.0f;
    bool isGround = true;

    //앉기
    bool isCrouch = false;
    //콜라이더 크기 조절
    float colOriginHeight;
    float colCrouchHeight = 1.0f;

    bool playerStop = false;

    void Start()
    {
        ani = arm.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        weaponController.SetCrossHair(crossHair);

        colOriginHeight = col.height;

        SetCharacterStat(100.0f, 15.0f);
        runSpeed = walkSpeed * 1.5f;
        crouchSpeed = walkSpeed * 0.6f;
        curSpeed = walkSpeed;
    }

    void Update()
    {
        if (playerStop) return;

        //플레이어 이동      
        PlayerRotation();
        PlayerMove();
        IsGround();
        PlayerJump();
        PlayerRun();
        PlayerCrouch();

        //사격
        Fire();
        Reload();
    }

    //이동
    void PlayerMove()
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        float zDir = Input.GetAxisRaw("Vertical");

        Vector3 horizontal = transform.right * xDir;
        Vector3 vertical = transform.forward * zDir;

        Vector3 velocity = (horizontal + vertical).normalized * curSpeed;

        rb.MovePosition(transform.position + velocity * Time.deltaTime);

        if (velocity.magnitude == 0) isWalk = false;
        else isWalk = true;

        //ani.SetBool("isWalking", isWalk);
    }

    void PlayerRotation()
    {
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 playerRot = new Vector3(0.0f, yRot, 0.0f) * mainCamera.GetComponent<MainCamera>().GetCameraSensitivity();

        rb.MoveRotation(rb.rotation * Quaternion.Euler(playerRot));
    }

    void PlayerRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && weaponController.CanFire() == false) Running();
        if (Input.GetKeyUp(KeyCode.LeftShift)) StopRunning();

        ani.SetBool("IsRunning", isRun);
    }

    void Running()
    {
        isRun = true;
        curSpeed = runSpeed;
    }

    void StopRunning()
    {
        isRun = false;
        curSpeed = walkSpeed;
    }

    //점프
    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true) Jump();
    }

    void Jump()
    {
        rb.velocity = transform.up * jumpForce;
    }

    void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    //앉기
    void PlayerCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouch = true;
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouch = false;
            Crouch();
        }
    }

    void Crouch()
    {
        if (isCrouch)
        {
            curSpeed = crouchSpeed;
            col.height = colCrouchHeight;
        }
        else
        {
            curSpeed = walkSpeed;
            col.height = colOriginHeight;
        }
    }

    //무기관련
    void Fire()
    {
        if (Input.GetMouseButton(0) && weaponController.CanFire() == true && isRun == false)
        {
            weaponController.Fire(true);
            crossHair.StartFireAnimation();
            ani.SetBool("IsFire", true);
        }
        if (Input.GetMouseButtonUp(0) || weaponController.CanFire() == false)
        {
            crossHair.StopFireAnimation();
            ani.SetBool("IsFire", false);
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) || weaponController.GetCurMagazine() <= 0)
            weaponController.Reload();

        ani.SetBool("IsReloading", weaponController.IsReload());
    }

    //크로스헤어
    void CrossHairState()
    {
        crossHair.SetCrouchAnimation(isCrouch);
        crossHair.SetRunAnimation(isRun);
        crossHair.SetWalkAnimation(isWalk);
    }
}