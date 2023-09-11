using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;




    bool isLive;

    private Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate waitForFixedUpdate;
    Animator animator;
    Collider2D coll;




    public Rigidbody2D Rigid { get => target; set => target = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }





    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        waitForFixedUpdate = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
        isLive = true;
    }


    void FixedUpdate()
    {
        if(!GameManager.instance.IsLive) return;

        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        if (dirVec.magnitude > 50f)
        {
            gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        if(!GameManager.instance.IsLive) return;

        if (!isLive)
        {
            return;
        }

        if (target.position.x < rigid.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnEnable(){
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        animator.SetBool("Dead", false);
        spriteRenderer.sortingOrder = 2;
        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") || !isLive)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            health -= bullet.Damage;
            StartCoroutine(KnockBack());
            

            if(health > 0){
                animator.SetTrigger("Hit");
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
            }
            else{
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                animator.SetBool("Dead", true);
                spriteRenderer.sortingOrder = 1;
                GameManager.instance.Kill++;
                GameManager.instance.GetExp();

                if(GameManager.instance.IsLive){
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
                }
            }
        }
    }

    IEnumerator KnockBack()
    {
        yield return waitForFixedUpdate;
        Vector3 playerPos = GameManager.instance.Player.transform.position;
        Vector3 dirVec = (transform.position - playerPos).normalized;
        rigid.AddForce(dirVec * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);

    }
}