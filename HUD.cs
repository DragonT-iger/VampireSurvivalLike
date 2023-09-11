using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level , Kill , Time , Health}

    [SerializeField] private InfoType infoType;

    Text myText;
    Slider mySlider;

    void Awake(){
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate(){
        switch(infoType){
            case InfoType.Exp:
                float currExp = GameManager.instance.Exp;
                float nextExp = GameManager.instance.NextExp[Mathf.Min(GameManager.instance.Level, GameManager.instance.NextExp.Length - 1)];

                mySlider.value = currExp / nextExp;

                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.Level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.Kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.MaxGameTime - GameManager.instance.GameTime;
                int min = Mathf.FloorToInt(remainTime / 60f);
                int sec = Mathf.FloorToInt(remainTime % 60f);

                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);

                break;
            case InfoType.Health:

                float currHealth = GameManager.instance.Health;
                float maxHealth = GameManager.instance.MaxHealth;

                mySlider.value = currHealth / maxHealth;

                break;
        }
    }
}
