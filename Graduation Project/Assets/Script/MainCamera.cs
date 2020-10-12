using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform playerPos;

    //거리
    float dist = 0.0f;

    //민감도
    float sensitivity = 2.0f;

    //회전
    float rotLimit = 45.0f;
    float curRotX = 0.0f;
    float curRotY = 0.0f;

    bool cameraStop = false;

    void Start()
    {
        playerPos = GameObject.Find("Player").transform;

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

        curRotX -= x_rot * sensitivity;
        curRotY += y_rot * sensitivity;

        curRotX = Mathf.Clamp(curRotX, -rotLimit, rotLimit);

        this.transform.localEulerAngles = new Vector3(curRotX, curRotY, 0.0f);
        this.transform.position = Quaternion.Euler(curRotX, curRotY, 0) * new Vector3(0, playerPos.position.y - 1.08f, -dist) + playerPos.position;
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
