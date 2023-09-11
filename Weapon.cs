using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count; // Bullet Count, Penetratation Count
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        player = GameManager.instance.Player;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if(!GameManager.instance.IsLive) return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime * 120f);
                break;
            default:
                timer += Time.deltaTime;

                float divide = 1f / speed;

                if (timer > divide)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

    }

    public void LevelUp(float damage, int count)
    {
        this.count += count;

        if(id == 0)
        {
            Batch();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;
        speed = data.baseSpeed;

        for(int index = 0; index < GameManager.instance.PoolManager.BulletPrefabs.Length; index++)
        {
            if(GameManager.instance.PoolManager.BulletPrefabs[index] == data.projectile)
            {
                prefabId = index;
                break;
            }
        }

        if(id == 0)
        {
            Batch();
        }


    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            //
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.PoolManager.GetBullet(prefabId).transform;
                bullet.parent = transform;
            }
            
            bullet.parent = transform;

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero); // -1 is Infinity Per.
        }
    }

    void Fire()
    {
        if (!player.Scanner.nearestTarget)
            return;

        Vector3 targetPso = player.Scanner.nearestTarget.position;
        Vector3 dir = (targetPso - transform.position).normalized;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.PoolManager.GetBullet(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);

    }



}