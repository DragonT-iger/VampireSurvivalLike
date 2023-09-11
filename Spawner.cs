using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform[] spawnPoints;
    [SerializeField] private LevelInfo[] levelInfos; // 몬스터 스폰되는 레벨들의 정보
    [SerializeField] private int level = 0; // 현재 레벨

    void Awake(){
        spawnPoints = GetComponentsInChildren<Transform>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.IsLive) return;

        foreach(SpawnData spawnData in levelInfos[level].SpawnDatas){
            spawnData.Timer += Time.deltaTime;
            if(spawnData.Timer > spawnData.SpawnTime){
                Spawn(spawnData.SpriteType);
                spawnData.Timer = 0f;
            }
        }

        //space를 누르면 level이 1 증가 (TestCode)

        // if(Input.GetKeyDown(KeyCode.Space)){
        //     level++;
        //     Debug.Log("Level : " + level);
        // }
    }

    void Spawn(int Unit){
        int index = Random.Range(1, spawnPoints.Length);
        GameManager.instance.PoolManager.SpawnEnemy(Unit, spawnPoints[index].position);
    }

}

[System.Serializable]
public class LevelInfo{
    [SerializeField] private SpawnData[] spawnDatas;

    public SpawnData[] SpawnDatas { get => spawnDatas; set => spawnDatas = value; }
}


[System.Serializable]
public class SpawnData
{
    [SerializeField] private int spriteType;
    [SerializeField] private float spawnTime;
    [SerializeField] private float timer;
    [SerializeField] private int health;


    public int SpriteType { get => spriteType; set => spriteType = value; }
    public float SpawnTime { get => spawnTime; set => spawnTime = value; }
    public float Timer { get => timer; set => timer = value; }
    public int Health { get => health; set => health = value; }
}
