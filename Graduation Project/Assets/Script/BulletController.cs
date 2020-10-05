using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    GameObject Bullet_Prefab;
    List<GameObject> All_Bullet = new List<GameObject>();

    [SerializeField]
    GameObject Main_Camera;

    //총알 설정
    public void SetBullet(int bullet_num, float accuracy, float range, float speed, float damage)
    {
        for (int i = 0; i < bullet_num; i++)
        {
            GameObject Obj = Instantiate(Bullet_Prefab) as GameObject;
            Obj.transform.position = transform.position;
            Obj.GetComponent<Bullet>().SetBullet(accuracy, range, speed, damage);
            All_Bullet.Add(Obj);
        }
    }

    //사격
    public void Fire()
    {
        GameObject Obj = GetUnusedBullet();
        Obj.SetActive(true);
        Obj.transform.rotation = Main_Camera.transform.rotation;
        Obj.transform.position = transform.position;
        Obj.GetComponent<Bullet>().Fire();
    }

    //사용할 수 있는 총알 찾기
    GameObject GetUnusedBullet()
    {
        foreach (var Obj in All_Bullet)
            if (Obj.activeSelf == false)
                return Obj;

        return null;
    }

}
