using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject Main_Camera;   

    //컴포넌트
    Animator Animator;
    Rigidbody Rb;
    CapsuleCollider this_Col;

    //이동
    float cur_speed;
    float run_speed = 60.0f;
    float walk_speed = 40.0f;
    float crouch_speed = 30.0f;
    bool is_run = false;
    float jump_force = 10.0f;
    bool is_ground = true;

    //앉기
    float crouch_pos_y;
    float origin_pos_y;
    float cur_crouch_pos_y;
    bool is_crouch = false;

    bool player_stop = false;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        this_Col = GetComponent<CapsuleCollider>();
        Main_Camera = GameObject.Find("Main Camera");

        cur_speed = walk_speed;

        origin_pos_y = Main_Camera.transform.localPosition.y;
        cur_crouch_pos_y = origin_pos_y;
        crouch_pos_y = this_Col.bounds.extents.y;
    }

    void Update()
    {
        if (player_stop) return;

        //플레이어 이동      
        IsGround();
        PlayerJump();
        PlayerRun();
        PlayerCrouch();
        PlayerRotation();
        PlayerMove();        
    }

    void PlayerMove()
    {
        float x_dir = Input.GetAxisRaw("Horizontal");
        float z_dir = Input.GetAxisRaw("Vertical");

        Vector3 horizontal = transform.right * x_dir;
        Vector3 vertical = transform.forward * z_dir;

        Vector3 velocity = (horizontal + vertical).normalized * cur_speed;

        Rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }
    
    void PlayerRotation()
    {
        float y_rot = Input.GetAxisRaw("Mouse X");
        Vector3 player_rot_y = new Vector3(0.0f, y_rot, 0.0f) * Main_Camera.GetComponent<MainCamera>().GetCameraSensitivity();

        Rb.MoveRotation(Rb.rotation * Quaternion.Euler(player_rot_y));
    }

    void PlayerRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopRunning();
        }
    }

    void Running()
    {
        is_run = true;
        cur_speed = run_speed;
    }

    void StopRunning()
    {
        is_run = false;
        cur_speed = walk_speed;
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && is_ground == true)
        {
            Jump();
        }
    }

    void Jump()
    {
        Rb.velocity = transform.up * jump_force;
    }

    void IsGround()
    {
        //capsule collider의 반 만큼 레이 쏘기
        is_ground = Physics.Raycast(transform.position, Vector3.down, this_Col.bounds.extents.y + 0.1f);
    }

    void PlayerCrouch()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            is_crouch = true;
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            is_crouch = false;
            Crouch();
        }
    }

    void Crouch()
    {
        if(is_crouch)
        {
            cur_speed = crouch_speed;
            cur_crouch_pos_y = crouch_pos_y;
        }
        else
        {
            cur_speed = walk_speed;
            cur_crouch_pos_y = origin_pos_y;
        }

        StartCoroutine(CrouchCoroutine());
    }

    IEnumerator CrouchCoroutine()
    {
        float pos_y = Main_Camera.transform.localPosition.y;
        int count = 0;
        
        while(pos_y != cur_crouch_pos_y)
        {
            count++;
            pos_y = Mathf.Lerp(pos_y, cur_crouch_pos_y, 0.4f);
            Main_Camera.transform.localPosition = new Vector3(Main_Camera.transform.localPosition.x, pos_y, Main_Camera.transform.localPosition.z);
            //15번 실행 후 보간 끝내기
            if (count > 20)
                break;
            yield return null;
        }
        Main_Camera.transform.localPosition = new Vector3(Main_Camera.transform.localPosition.x, cur_crouch_pos_y, Main_Camera.transform.localPosition.z);
    }
}