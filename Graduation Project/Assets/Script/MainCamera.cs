using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform player_pos;

    //거리
    float dist = 1.0f;

    //민감도
    float sensitivity = 2.0f;

    //회전
    float rot_limit = 45.0f;
    float cur_rot_x = 0.0f;
    float cur_rot_y = 0.0f;

    bool camera_stop = false;

    void Start()
    {
        player_pos = GameObject.Find("Player").transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; //커서 고정
    }

    void Update()
    {
        CameraMove();
    }

    void CameraMove()
    {
        float x_rot = Input.GetAxisRaw("Mouse Y");
        float y_rot = Input.GetAxisRaw("Mouse X");

        cur_rot_x -= x_rot * sensitivity;
        cur_rot_y += y_rot * sensitivity;

        cur_rot_x = Mathf.Clamp(cur_rot_x, -rot_limit, rot_limit);

        this.transform.localEulerAngles = new Vector3(cur_rot_x, cur_rot_y, 0.0f);
        this.transform.position = Quaternion.Euler(cur_rot_x, cur_rot_y, 0) * new Vector3(0, player_pos.position.y + 2.5f, -dist) + player_pos.position;
    }

    //public void CameraStop()
    //{
    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.None; //커서 풀기

    //    camera_stop = true;
    //}

    //public void CameraPlay()
    //{
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked; //커서 고정

    //    camera_stop = false;
    //}

    public float GetCameraSensitivity() { return sensitivity; }
}
