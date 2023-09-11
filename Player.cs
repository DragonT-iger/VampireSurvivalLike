using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private Vector2 inputVec;
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rigid;
    private Scanner scanner;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public RuntimeAnimatorController[] animators;


    //getter setter

    public Vector2 InputVec { get => inputVec; set => inputVec = value; }
    public float Speed { get => speed; set => speed = value; }
    public Rigidbody2D Rigid { get => rigid; set => rigid = value; }
    public Scanner Scanner { get => scanner; set => scanner = value; }


    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();

    }

    void OnMove(InputValue value){
        inputVec = value.Get<Vector2>();
    }

    void OnEnable()
    {
        animator.runtimeAnimatorController = animators[GameManager.instance.PlayerId];
    }

    void FixedUpdate(){

        if(!GameManager.instance.IsLive) return;
        rigid.MovePosition(rigid.position + inputVec * Time.fixedDeltaTime * speed);
    }

    void LateUpdate(){

        if(!GameManager.instance.IsLive) return;

        animator.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0){
            spriteRenderer.flipX = inputVec.x < 0;
        }
    }

    void OnCollisionStay2D(Collision2D other){
        if(!GameManager.instance.IsLive) return;

        if(other.gameObject.CompareTag("Enemy")){
            GameManager.instance.Health -= Time.deltaTime * 10f;
        }

        if(GameManager.instance.Health <= 0f){
            for(int index=2; index < transform.childCount; index++){
                transform.GetChild(index).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
