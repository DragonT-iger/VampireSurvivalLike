using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    // public ItemData.ItemType type;
    // public float rate;

    // public void Init(ItemData itemData)
    // {
    //     name = "Gear " + itemData.itemId;
    //     transform.parent = GameManager.instance.Player.transform;
    //     transform.localPosition = Vector3.zero;

    //     type = itemData.itemType;
    //     rate = itemData.damages[0];
    // }

    // public void LevelUp(float rate)
    // {
    //     this.rate = rate;
    //     ApplyGear();
    // }

    // void ApplyGear()
    // {
    //     switch(type){
    //         case ItemData.ItemType.Glove:
    //             RateUp();
    //             break;
    //         case ItemData.ItemType.Shoe:
    //             SpeedUp();
    //             break;
    //     }
    // }

    // void RateUp()
    // {
    //     Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

    //     foreach(Weapon weapon in weapons){
    //         switch(weapon.id){
    //             case 0:
    //                 weapon.speed = 1 + (1 * rate);
    //                 break;
    //             default:
    //                 weapon.speed = 0.5f * (1f + rate);
    //                 break;
    //         }
    //     }
    // }


    // void SpeedUp()
    // {
    //     float speed = 3;
    //     GameManager.instance.Player.Speed = speed + speed * rate;
    // }
}
