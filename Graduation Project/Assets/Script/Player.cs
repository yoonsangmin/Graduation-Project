using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    [SerializeField]
    GameObject mainCamera;
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
    float crouchPosY;
    float originPosY;
    float curCrouchPosY;
    bool isCrouch = false;

    bool playerStop = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        SetCharacterStat(100.0f, 10.0f);
        runSpeed = walkSpeed * 1.5f;
        crouchSpeed = walkSpeed * 0.6f;
        curSpeed = walkSpeed;

        originPosY = mainCamera.transform.localPosition.y;
        curCrouchPosY = originPosY;
        crouchPosY = col.bounds.extents.y;
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
    }

    void PlayerRotation()
    {
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 playerRot = new Vector3(0.0f, yRot, 0.0f) * mainCamera.GetComponent<MainCamera>().GetCameraSensitivity();

        float xRot = Input.GetAxisRaw("Mouse Y");
        xRot = Mathf.Clamp(xRot, -45, 45);

        arm.transform.rotation = arm.transform.rotation * Quaternion.Euler(new Vector3(-xRot, 0.0f, 0.0f) * mainCamera.GetComponent<MainCamera>().GetCameraSensitivity());

        rb.MoveRotation(rb.rotation * Quaternion.Euler(playerRot));
    }

    void PlayerRun()
    {
        if (Input.GetKey(KeyCode.LeftShift)) Running();
        if (Input.GetKeyUp(KeyCode.LeftShift)) StopRunning();
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
        //capsule collider의 반 만큼 레이 쏘기
        isGround = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
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
            curCrouchPosY = crouchPosY;
        }
        else
        {
            curSpeed = walkSpeed;
            curCrouchPosY = originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    IEnumerator CrouchCoroutine()
    {
        float pos_y = mainCamera.transform.localPosition.y;
        int count = 0;

        while (pos_y != curCrouchPosY)
        {
            count++;
            pos_y = Mathf.Lerp(pos_y, curCrouchPosY, 0.4f);
            mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, pos_y, mainCamera.transform.localPosition.z);
            //20번 실행 후 보간 끝내기
            if (count > 20)
                break;
            yield return null;
        }

        mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, curCrouchPosY, mainCamera.transform.localPosition.z);
    }

    //플레이어 상태 전하는 함수
    public bool IsPlayerWalk() { return isWalk; }
    public bool IsPlayerRun() { return isRun; }
    public bool IsPlayerCrouch() { return isCrouch; }
}