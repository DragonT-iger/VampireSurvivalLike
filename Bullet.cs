using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem.LowLevel;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private float damage;
    [SerializeField] private int penetratationCount;

    Rigidbody2D rigid;

    Player player;
    public float Damage { get => damage; set => damage = value; }
    public int PenetratationCount { get => penetratationCount; set => penetratationCount = value; }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameManager.instance.Player;
    }

    void Update()
    {

        if(id == 0){
            if (Vector3.Distance(transform.position, player.transform.position) > 15f)
            {
                gameObject.SetActive(false);
            }
        }
        
    }


    

    public void Init(float damage, int penetratationCount, Vector3 dir)
    {
        this.damage = damage;
        this.penetratationCount = penetratationCount;

        if(!IsMelee(penetratationCount)){
            rigid.velocity = dir * 15f;
        }
    }


    private bool IsMelee(int penetratationCount){
        if(penetratationCount <= -1){
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || penetratationCount <= -1)
        {
            return;
        }

        penetratationCount--;

        if(penetratationCount < 0){
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
