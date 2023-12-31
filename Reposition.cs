using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other){

        if(!other.CompareTag("Area"))
            return;
        
        Vector3 playerPos = GameManager.instance.Player.transform.position;
        Vector3 myPos = transform.position;

        

        switch (transform.tag){
            
            case "Ground":

                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;

                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);
            
                if(diffX > diffY){
                    transform.Translate(Vector3.right* dirX * 2 * 22);
                }
                else if(diffX < diffY){
                    transform.Translate(Vector3.up* dirY * 2 * 22);
                }
                break;
            case "Enemy":
                break;
        }
    }
}
