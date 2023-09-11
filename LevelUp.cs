using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;


    void Awake(){
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }


    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();

        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        //1. 모든 아이템 비활성화
        foreach(Item item in items){
            item.gameObject.SetActive(false);
        }


        //2. 그중에서 랜덤 3개 아이템 활성화

        List<int> itemlist = new List<int>();

        for(int i = 0 ; i < items.Length ; i++)
        {
            itemlist.Add(i);
        }

        for(int i = 0 ; i < 3 ; i++)
        {
            int index = Random.Range(0, itemlist.Count);

            if(items[itemlist[index]])

            if(items[itemlist[index]].IsMaxLevel)
            {
                items[4].gameObject.SetActive(true);
                itemlist.RemoveAt(index);
            }
            else
            {
                items[itemlist[index]].gameObject.SetActive(true);
                itemlist.RemoveAt(index);
            }
        }

        

        //combination을 이용해서 items의 개수 , 3개를 뽑고 그 크기만큼 

        //3. 만렙 아이템의 경우는 소비아이템으로 대체
    }

}
