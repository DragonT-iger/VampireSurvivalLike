using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("#Game Control")]
    [SerializeField] private bool isLive;
    [SerializeField] private float gameTime = 0f;
    [SerializeField] private float maxGameTime = 60f;

    [Header("#Player Info")]
    [SerializeField] private int playerId;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private int level;
    [SerializeField] private int kill;
    [SerializeField] private int exp;
    [SerializeField] private int[] nextExp = {3, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 120};

    [Header("# Game Object")]
    
    [SerializeField] private Player player;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private LevelUp uiLevelUp;
    [SerializeField] private Result uiResult;
    [SerializeField] private Transform uiJoyStick;
    [SerializeField] private GameObject enemyCleaner;
    


    public bool IsLive { get => isLive; set => isLive = value; }
    public float GameTime { get => gameTime; set => gameTime = value; }
    public float MaxGameTime { get => maxGameTime; set => maxGameTime = value; }
    public Player Player { get => player; set => player = value; }
    public PoolManager PoolManager { get => poolManager; set => poolManager = value; }
    public int PlayerId { get => playerId; set => playerId = value; }
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Level { get => level; set => level = value; }
    public int Kill { get => kill; set => kill = value; }
    public int Exp { get => exp; set => exp = value; }
    public int[] NextExp { get => nextExp; set => nextExp = value; }









    void Awake(){
        instance = this;
        Application.targetFrameRate = 60;
    }

    void Update(){

        if(!isLive) return;

        gameTime += Time.deltaTime;

        if(gameTime >= maxGameTime - 1){
            GameVictory();
        }
    }

    public void GameStart(int id){
        playerId = id;
        health = maxHealth;
        uiLevelUp.Select(playerId % 2); // 임시

        player.gameObject.SetActive(true);
        Time.timeScale = 1f;
        uiJoyStick.localScale = Vector3.one;
        isLive = true;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.PlayBgm(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameOver(){
        StartCoroutine(GameOverRoutine());

        
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Time.timeScale = 0f;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
        AudioManager.instance.PlayBgm(false);
    }


    public void GameVictory(){
        StartCoroutine(GameVictoryRoutine());

    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(1f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Time.timeScale = 0f;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
        AudioManager.instance.PlayBgm(false);
    }


    public void GetExp()
    {

        if(!isLive) return;

        exp++;

        if(exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)]){
            LevelUp();
            uiLevelUp.Show();
        }
    }

    private void LevelUp()
    {
        level++;
        exp = 0;
    }

    public void Stop(){
        isLive = false;
        Time.timeScale = 0f;
        uiJoyStick.localScale = Vector3.zero;
    }

    public void Resume(){
        isLive = true;
        Time.timeScale = 1f;
        uiJoyStick.localScale = Vector3.one;
    }

}
