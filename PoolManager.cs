using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] Enemyprefabs;
    public GameObject[] BulletPrefabs;

    List<GameObject>[] enemyPools;

    List<GameObject>[] bulletPools;

    void Awake()
    {
        enemyPools = new List<GameObject>[Enemyprefabs.Length];

        for(int i = 0; i < Enemyprefabs.Length; i++){
            enemyPools[i] = new List<GameObject>();
        }

        bulletPools = new List<GameObject>[BulletPrefabs.Length];

        for(int i = 0; i < BulletPrefabs.Length; i++){
            bulletPools[i] = new List<GameObject>();
        }

    }

    public GameObject SpawnEnemy(int index , Vector3 spawnPosition){
        GameObject select = null;

        foreach(GameObject item in enemyPools[index]){
            if(!item.activeSelf){
               select = item;
               select.transform.position = spawnPosition;
               select.SetActive(true);
               break;
            }
        }
        if(!select){
            select = Instantiate(Enemyprefabs[index], spawnPosition, Quaternion.identity);
            select.GetComponent<EnemyController>().Rigid = GameManager.instance.Player.Rigid;
            enemyPools[index].Add(select);
        }

        return select;
    }

    public void ChangeSpeed(int spriteType, float speed){
        Enemyprefabs[spriteType].GetComponent<EnemyController>().Speed = speed;
    }

    public GameObject GetBullet(int index){
        GameObject select = null;

        foreach(GameObject item in bulletPools[index]){
            if(!item.activeSelf){
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if(!select){
            select = Instantiate(BulletPrefabs[index]);
            bulletPools[index].Add(select);
        }

        return select;
    }

   
}
