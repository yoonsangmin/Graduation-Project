using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    Image player;
    [SerializeField]
    List<Image> enemys;

    [SerializeField]
    Transform playerPos;
    [SerializeField]
    List<Transform> enemysPos;

    float miniMapWidth;
    float miniMapHeight;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void PlayerImageRot(float yRot)
    {
        player.transform.rotation = Quaternion.Euler(0.0f, yRot, 0.0f);
    }

    void EnemysImagePos(Vector3 playerPos,Vector3 enemyPos)
    {
        if(Vector3.Distance(playerPos, enemyPos) <= 50.0f)
        {

        }
        else
        {

        }
    }
}
