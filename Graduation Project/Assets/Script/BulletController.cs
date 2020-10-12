using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    GameObject gunEntry;
    [SerializeField]
    GameObject bullet;
    List<GameObject> bullets = new List<GameObject>();

    [SerializeField]
    GameObject mainCamera;

    //총알 설정
    public void SetBullet(int bulletNum, float accuracy, float range, float speed, float damage)
    {
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject obj = Instantiate(bullet) as GameObject;
            obj.transform.position = transform.position;
            obj.transform.parent = transform;
            obj.GetComponent<Bullet>().SetBullet(accuracy, range, speed, damage);
            bullets.Add(obj);
        }
    }

    //사격
    public void Fire()
    {
        GameObject obj = GetUnusedBullet();
        obj.SetActive(true);
        obj.transform.parent = transform;
        obj.transform.rotation = mainCamera.transform.rotation;
        obj.transform.position = gunEntry.transform.position;
        obj.GetComponent<Bullet>().Fire();
    }

    //사용할 수 있는 총알 찾기
    GameObject GetUnusedBullet()
    {
        foreach (var obj in bullets)
            if (obj.activeSelf == false)
                return obj;

        return null;
    }

}
