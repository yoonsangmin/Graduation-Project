using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform playerPos;

    //민감도
    float sensitivity = 2.0f;

    //회전
    float rotLimit = 45.0f;
    float curRotX = 0.0f;

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

        curRotX -= x_rot * sensitivity;

        curRotX = Mathf.Clamp(curRotX, -rotLimit, rotLimit);

        this.transform.localEulerAngles = new Vector3(curRotX, 0.0f, 0.0f);
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
    public void DoRecoilAction(float value) { curRotX -= value; }
}
