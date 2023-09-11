using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int level;
    [SerializeField] Weapon weapon;
    [SerializeField] private Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;



    public ItemData ItemData{ get => itemData; set => itemData = value; }
    public int Level { get => level; set => level = value; }
    public Weapon Weapon { get => weapon; set => weapon = value; }
    public Gear Gear { get => gear; set => gear = value; }



    void Awake(){
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = itemData.itemIcon;
        textLevel = GetComponentInChildren<Text>();

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        textName.text = itemData.itemName;
    }


    void OnEnable(){
        textLevel.text = "Lv." + (level + 1);
        switch(itemData.itemType){
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(itemData.itemDescription,itemData.damages[level]*100, itemData.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(itemData.itemDescription,itemData.damages[level]*100);
                break;
            default:
                textDesc.text = string.Format(itemData.itemDescription);
                break;
        }
    }

    public void OnClick(){
        switch(itemData.itemType){
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0){
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(itemData);
                }
                else{
                    float nextDamage = itemData.baseDamage * itemData.damages[level];
                    int nextCount = itemData.counts[level];
                    float nextSpeed = itemData.baseSpeed * itemData.speeds[level];

                    weapon.speed = nextSpeed;
                    weapon.LevelUp(nextDamage, nextCount);
                }
                break;
            case ItemData.ItemType.Glove:
                // if(level == 0){
                //     GameObject newGear = new GameObject();
                //     gear = newGear.AddComponent<Gear>();
                //     gear.Init(itemData);
                // }
                // else{
                //     float nextRate = itemData.damages[level];
                //     gear.LevelUp(nextRate);
                // }

                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Heal:
                break;
        }
        level++;

        if(level == itemData.damages.Length){
            GetComponent<Button>().interactable = false;
        }
    }


    public bool IsMaxLevel{
        get{
            return level == itemData.damages.Length;
        }
    }
}
