using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    Image playerImage = null;
    [SerializeField]
    List<Image> enemysImage = new List<Image>();
    [SerializeField]
    Sprite enemyOutOfRangeImage = null;
    [SerializeField]
    Sprite enemyOriginImage = null;
    [SerializeField]
    Vector3 enemyHidePos = new Vector3(0.0f, 65.0f, 0.0f);

    [SerializeField]
    float dist = 85.0f;

    int enemysCount = 1;

    public void MiniMapUpdate(Transform playerPos, List<Enemy> enemysPos) { ObjectsImagePos(enemysPos, playerPos); }

    public void SetEnemysCount(int newEnemysCount)
    {
        enemysCount = newEnemysCount;
        ResetEnemysImage();
    }

    void ResetEnemysImage()
    {
        foreach (Image enemyImage in enemysImage)
        {
            enemyImage.gameObject.SetActive(true);
            enemyImage.sprite = enemyOriginImage;
            enemyImage.transform.localPosition = enemyHidePos;
        }
    }

    void ObjectsImagePos(List<Enemy> enemysPos, Transform playerPos)
    {
        //플레이어 이미지
        playerImage.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -(playerPos.eulerAngles.y - 90));

        //적 이미지
        for (int i = 0; i < enemysCount; i++)
        {
            if (enemysImage[i].gameObject.activeSelf == false) continue;
            if (enemysPos[i].IsDead() == true)
            {
                enemysImage[i].gameObject.SetActive(false);
                continue;
            }

            Vector2 playerXZPos = new Vector2(playerPos.position.z, playerPos.position.x);
            Vector2 enemyXZPos = new Vector2(enemysPos[i].transform.position.z, enemysPos[i].transform.position.x);

            if (Vector2.Distance(playerXZPos, enemyXZPos) <= dist)
            {
                enemysImage[i].sprite = enemyOriginImage;
                enemysImage[i].transform.position = new Vector3(playerImage.transform.position.x - (enemyXZPos.x - playerXZPos.x), playerImage.transform.position.y + (enemyXZPos.y - playerXZPos.y), 0.0f);
            }
            else
                enemysImage[i].sprite = enemyOutOfRangeImage;
        }
    }
}
